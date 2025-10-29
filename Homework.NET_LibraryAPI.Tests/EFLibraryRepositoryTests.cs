using Homework.NET_LibraryAPI.Data;
using Homework.NET_LibraryAPI.Models.DTO;
using Homework.NET_LibraryAPI.Models;
using Homework.NET_LibraryAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Xunit;

namespace Homework.NET_LibraryAPI.Tests
{
    public class EFLibraryRepositoryTests
    {
        private LibraryContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<LibraryContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var context = new LibraryContext(options);
            return context;
        }

        [Fact]
        public async Task GetAllAuthors_ShouldReturnAuthorDtoWithBookCount()
        {
            // ARRANGE
            using var context = GetInMemoryDbContext();
            var repo = new EFLibraryRepository(context);
            var token = CancellationToken.None;

            var author1 = await repo.CreateAuthorAsync(new Author { Name = "Пушкин", DateOfBirth = 1800 } , token);
            await repo.CreateBookAsync(new Book { Title = "Книга A", PublishedYear = 1820, AuthorId = author1.Id }, token);
            await repo.CreateBookAsync(new Book { Title = "Книга B", PublishedYear = 1821, AuthorId = author1.Id }, token);
            await repo.CreateAuthorAsync(new Author { Name = "Лермонтов", DateOfBirth = 1814 }, token);

            // ACT
            var result = await repo.GetAllAuthorsAsync(token);

            // ASSERT
            Assert.Equal(2, result.Count);
            var pushkin = result.SingleOrDefault(a => a.Name!.Contains("Пушкин"));
            Assert.NotNull(pushkin);
            const string expectedName = "Пушкин ( Количество книг этого автора в нашей библиотеке: 2 )";
            Assert.Equal(expectedName, pushkin!.Name);
            Assert.Equal(2, pushkin.Books.Count);
        }

        [Theory]
        [InlineData(1950, 1)]
        [InlineData(1800, 5)]
        public async Task GetAuthorsBornAfter_ShouldReturnCorrectFilteredList(int year, int expectedCount)
        {
            // ARRANGE
            using var context = GetInMemoryDbContext();
            var repo = new EFLibraryRepository(context);
            var token = CancellationToken.None;

            await repo.CreateAuthorAsync(new Author { Name = "Достоевский", DateOfBirth = 1821 }, token);
            await repo.CreateAuthorAsync(new Author { Name = "Дойль", DateOfBirth = 1859 }, token);
            await repo.CreateAuthorAsync(new Author { Name = "Оруэлл", DateOfBirth = 1903 }, token);
            await repo.CreateAuthorAsync(new Author { Name = "Этвуд", DateOfBirth = 1939 }, token);
            await repo.CreateAuthorAsync(new Author { Name = "Мартин", DateOfBirth = 1952 }, token);
            await repo.CreateAuthorAsync(new Author { Name = "Пушкин", DateOfBirth = 1800 }, token); 

            // ACT
            var result = await repo.GetAuthorsBornAfterAsync(year, token);

            // ASSERT
            Assert.Equal(expectedCount, result.Count);
            Assert.All(result, a => Assert.True(a.DateOfBirth > year));
        }

        [Fact]
        public async Task GetBookById_ShouldReturnBookDetailsDtoWithNestedAuthor()
        {
            // ARRANGE
            using var context = GetInMemoryDbContext();
            var repo = new EFLibraryRepository(context);
            var token = CancellationToken.None;
            var author = await repo.CreateAuthorAsync(new Author { Name = "Тестовый Автор", DateOfBirth = 1970 }, token);
            var book = await repo.CreateBookAsync(new Book { Title = "Тест-Книга", PublishedYear = 2000, AuthorId = author.Id }, token);

            // ACT
            var result = await repo.GetBookByIdAsync(book.Id, token);

            // ASSERT
            Assert.NotNull(result);
            Assert.IsType<BookDetailsDto>(result);
            Assert.NotNull(result.Author);
            Assert.Equal("Тестовый Автор", result.Author!.Name);
            Assert.IsType<AuthorNestedDto>(result.Author);
        }
    }
}
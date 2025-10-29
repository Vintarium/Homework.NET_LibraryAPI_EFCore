using Homework.NET_LibraryAPI.Models;
using Homework.NET_LibraryAPI.Models.DTO;
using Homework.NET_LibraryAPI.Repositories.Interfaces;
using Homework.NET_LibraryAPI.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework.NET_LibraryAPI.Tests
{
    public class BookServiceTests
    {
        private readonly Mock<ILibraryRepository> _mockRepo;
        private readonly BookService _bookService;
        private readonly CancellationToken _token = CancellationToken.None;

        public BookServiceTests()
        {
            _mockRepo = new Mock<ILibraryRepository>();
            _bookService = new BookService(_mockRepo.Object);
        }

        [Fact]
        public async Task CreateBookAsync_ShouldReturnDetailsDto_WhenAuthorExists()
        {
            // ARRANGE
            var bookDto = new BookCreationDto { Title = "Новая книга", PublishedYear = 2025, AuthorId = 1 };
            var createdBookModel = new Book { Id = 10, Title = bookDto.Title, AuthorId = bookDto.AuthorId };
            var expectedDetailsDto = new BookDetailsDto { Id = 10, Title = bookDto.Title };
            _mockRepo.Setup(repo => repo.GetAuthorByIdAsync(bookDto.AuthorId, _token))
                     .ReturnsAsync(new AuthorDetailsDto { Id = bookDto.AuthorId });
            _mockRepo.Setup(repo => repo.CreateBookAsync(It.IsAny<Book>(), _token))
                     .ReturnsAsync(createdBookModel);
            _mockRepo.Setup(repo => repo.GetBookByIdAsync(createdBookModel.Id, _token))
                     .ReturnsAsync(expectedDetailsDto);

            // ACT
            var result = await _bookService.CreateBookAsync(bookDto, _token);

            // ASSERT
            Assert.NotNull(result);
            _mockRepo.Verify(repo => repo.CreateBookAsync(It.IsAny<Book>(), _token), Times.Once);
            _mockRepo.Verify(repo => repo.GetAuthorByIdAsync(bookDto.AuthorId, _token), Times.Once);
        }

        [Fact]
        public async Task CreateBookAsync_ShouldReturnNull_WhenAuthorDoesNotExist()
        {
            // ARRANGE
            var bookDto = new BookCreationDto { Title = "Невалидная книга", PublishedYear = 2025, AuthorId = 999 };
            _mockRepo.Setup(repo => repo.GetAuthorByIdAsync(bookDto.AuthorId, _token))
                     .ReturnsAsync((AuthorDetailsDto?)null);
            // ACT
            var result = await _bookService.CreateBookAsync(bookDto, _token);

            // ASSERT
            Assert.Null(result);
            _mockRepo.Verify(repo => repo.CreateBookAsync(It.IsAny<Book>(), _token), Times.Never);
            _mockRepo.Verify(repo => repo.GetAuthorByIdAsync(bookDto.AuthorId, _token), Times.Once);
        }

        [Fact]
        public async Task UpdateBookAsync_ShouldReturnFalse_WhenNewAuthorDoesNotExist()
        {
            // ARRANGE
            var bookId = 1;
            var updateDto = new BookUpdateDto { Title = "Обновление", AuthorId = 999 };
            _mockRepo.Setup(repo => repo.GetBookByIdAsync(bookId, _token))
                     .ReturnsAsync(new BookDetailsDto { Id = bookId });
            _mockRepo.Setup(repo => repo.GetAuthorByIdAsync(updateDto.AuthorId, _token))
                     .ReturnsAsync((AuthorDetailsDto?)null);

            // ACT
            var result = await _bookService.UpdateBookAsync(bookId, updateDto, _token);

            // ASSERT
            Assert.False(result);
            _mockRepo.Verify(repo => repo.UpdateBookAsync(It.IsAny<Book>(), _token), Times.Never);
        }
    }
}

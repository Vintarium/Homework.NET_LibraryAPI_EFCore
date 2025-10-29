using Homework.NET_LibraryAPI.Models.DTO;
using Homework.NET_LibraryAPI.Models;
using Homework.NET_LibraryAPI.Repositories.Interfaces;
using Homework.NET_LibraryAPI.Services;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Homework.NET_LibraryAPI.Tests
{
    public class AuthorServiceTests
    {
        private readonly Mock<ILibraryRepository> _mockRepo;
        private readonly AuthorService _authorService;
        private readonly CancellationToken _token = CancellationToken.None;

        public AuthorServiceTests()
        {
            _mockRepo = new Mock<ILibraryRepository>();
            _authorService = new AuthorService(_mockRepo.Object);
        }

        [Fact]
        public async Task UpdateAuthorAsync_ShouldReturnTrue_WhenAuthorExists()
        {
            // ARRANGE
            var authorId = 1;
            var updateDto = new AuthorUpdateDto { Name = "Новое Имя", DateOfBirth = 2000 };
            _mockRepo.Setup(repo => repo.GetAuthorByIdAsync(authorId, _token))
                     .ReturnsAsync(new AuthorDetailsDto { Id = authorId });
            _mockRepo.Setup(repo => repo.UpdateAuthorAsync(It.IsAny<Author>(), _token))
                     .ReturnsAsync(true);
            // ACT
            var result = await _authorService.UpdateAuthorAsync(authorId, updateDto, _token);

            // ASSERT
            Assert.True(result);
            _mockRepo.Verify(
                repo => repo.UpdateAuthorAsync(
                    It.Is<Author>(a => a.Id == authorId && a.Name == updateDto.Name),
                    _token),
                Times.Once);
        }

        [Fact]
        public async Task UpdateAuthorAsync_ShouldReturnFalse_WhenAuthorDoesNotExist()
        {
            // ARRANGE
            var authorId = 999;
            var updateDto = new AuthorUpdateDto { Name = "Призрак", DateOfBirth = 1900 };

            _mockRepo.Setup(repo => repo.GetAuthorByIdAsync(authorId, _token))
                     .ReturnsAsync((AuthorDetailsDto?)null);
            // ACT
            var result = await _authorService.UpdateAuthorAsync(authorId, updateDto, _token);

            // ASSERT
            Assert.False(result);
            _mockRepo.Verify(
                repo => repo.UpdateAuthorAsync(It.IsAny<Author>(), _token),
                Times.Never);
        }
    }
}

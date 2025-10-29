using Homework.NET_LibraryAPI.Models;
using Homework.NET_LibraryAPI.Models.DTO;
using Homework.NET_LibraryAPI.Repositories.Interfaces;
using Homework.NET_LibraryAPI.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Homework.NET_LibraryAPI.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly ILibraryRepository _repo;
        public AuthorService(ILibraryRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<AuthorDto>> GetAllAuthorsAsync(CancellationToken cancellationToken) =>
            await _repo.GetAllAuthorsAsync(cancellationToken);
        public async Task<AuthorDetailsDto?> GetAuthorByIdAsync(int id, CancellationToken cancellationToken) =>
            await _repo.GetAuthorByIdAsync(id, cancellationToken);
        public async Task<List<AuthorDto>> GetAuthorsBornBeforeAsync(int year, CancellationToken cancellationToken) =>
            await _repo.GetAuthorsBornBeforeAsync(year, cancellationToken);
        public async Task<List<AuthorDto>> GetAuthorsBornAfterAsync(int year, CancellationToken cancellationToken) =>
            await _repo.GetAuthorsBornAfterAsync(year, cancellationToken);

        public async Task<AuthorDetailsDto> CreateAuthorAsync(AuthorCreationDto authorDto, CancellationToken cancellationToken)
        {
            var authorModel = new Author
            {
                Name = authorDto.Name,
                DateOfBirth = authorDto.DateOfBirth
            };
            var createdAuthor = await _repo.CreateAuthorAsync(authorModel, cancellationToken);
            return (await GetAuthorByIdAsync(createdAuthor.Id, cancellationToken))!;
        }

        public async Task<bool> UpdateAuthorAsync(int id, AuthorUpdateDto authorDto, CancellationToken cancellationToken)
        {
            if (await _repo.GetAuthorByIdAsync(id, cancellationToken) == null)
            {
                return false;
            }
            var authorModel = new Author
            {
                Id = id,
                Name = authorDto.Name,
                DateOfBirth = authorDto.DateOfBirth
            };
            return await _repo.UpdateAuthorAsync(authorModel, cancellationToken);
        }

        public async Task<bool> DeleteAuthorAsync(int id, CancellationToken cancellationToken)
        {
            return await _repo.DeleteAuthorAsync(id, cancellationToken);
        }
    }
}
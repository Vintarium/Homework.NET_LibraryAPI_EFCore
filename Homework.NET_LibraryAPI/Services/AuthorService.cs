using Homework.NET_LibraryAPI.Models;
using Homework.NET_LibraryAPI.Models.DTO;
using Homework.NET_LibraryAPI.Repositories.Interfaces;
using Homework.NET_LibraryAPI.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Homework.NET_LibraryAPI.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly ILibraryRepository _repo;
        public AuthorService(ILibraryRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<AuthorDto> GetAllAuthors() => _repo.GetAllAuthors();
        public AuthorDetailsDto? GetAuthorById(int id) => _repo.GetAuthorById(id);
        public IEnumerable<AuthorDto> GetAuthorsBornBefore(int year) => _repo.GetAuthorsBornBefore(year);
        public IEnumerable<AuthorDto> GetAuthorsBornAfter(int year) => _repo.GetAuthorsBornAfter(year);

        public AuthorDetailsDto CreateAuthor(AuthorCreationDto authorDto)
        {
            var authorModel = new Author
            {
                Name = authorDto.Name,
                DateOfBirth = authorDto.DateOfBirth
            };
            var createdAuthor = _repo.CreateAuthor(authorModel);
            return GetAuthorById(createdAuthor.Id)!;
        }

        public bool UpdateAuthor(int id, AuthorUpdateDto authorDto)
        {
            if (_repo.GetAuthorById(id) == null)
            {
                return false;
            }
            var authorModel = new Author
            {
                Id = id,
                Name = authorDto.Name,
                DateOfBirth = authorDto.DateOfBirth
            };
            return _repo.UpdateAuthor(authorModel);
        }

        public bool DeleteAuthor(int id)
        {
            return _repo.DeleteAuthor(id);
        }
    }
}
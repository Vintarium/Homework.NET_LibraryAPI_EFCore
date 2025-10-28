using Homework.NET_LibraryAPI.Models.DTO;

namespace Homework.NET_LibraryAPI.Services.Interfaces
{
    public interface IAuthorService
    {
        IEnumerable<AuthorDto> GetAllAuthors();
        AuthorDetailsDto? GetAuthorById(int id);
        IEnumerable<AuthorDto> GetAuthorsBornBefore(int year);
        IEnumerable<AuthorDto> GetAuthorsBornAfter(int year);
        AuthorDetailsDto CreateAuthor(AuthorCreationDto authorDto);
        bool UpdateAuthor(int id, AuthorUpdateDto authorDto);
        bool DeleteAuthor(int id);
    }
}

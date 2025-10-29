using Homework.NET_LibraryAPI.Models.DTO;

namespace Homework.NET_LibraryAPI.Services.Interfaces
{
    public interface IAuthorService
    {
       Task<List<AuthorDto>> GetAllAuthorsAsync(CancellationToken cancellationToken);
        Task<AuthorDetailsDto?> GetAuthorByIdAsync(int id, CancellationToken cancellationToken);
        Task<List<AuthorDto>> GetAuthorsBornBeforeAsync(int year, CancellationToken cancellationToken);
        Task<List<AuthorDto>> GetAuthorsBornAfterAsync(int year, CancellationToken cancellationToken);
        Task<AuthorDetailsDto> CreateAuthorAsync(AuthorCreationDto authorDto, CancellationToken cancellationToken);
        Task<bool> UpdateAuthorAsync(int id, AuthorUpdateDto authorDto, CancellationToken cancellationToken);
        Task<bool> DeleteAuthorAsync(int id, CancellationToken cancellationToken);
    }
}

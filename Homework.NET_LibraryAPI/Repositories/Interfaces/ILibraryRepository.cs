using Homework.NET_LibraryAPI.Models;
using Homework.NET_LibraryAPI.Models.DTO;

namespace Homework.NET_LibraryAPI.Repositories.Interfaces
{
    public interface ILibraryRepository
    {
        Task<List<AuthorDto>> GetAllAuthorsAsync(CancellationToken cancellationToken);
        Task<AuthorDetailsDto?> GetAuthorByIdAsync(int id, CancellationToken cancellationToken);
        Task<Author> CreateAuthorAsync(Author author, CancellationToken cancellationToken);
        Task<bool> UpdateAuthorAsync(Author author, CancellationToken cancellationToken);
        Task<bool> DeleteAuthorAsync(int id, CancellationToken cancellationToken);
        Task<List<AuthorDto>> GetAuthorsBornBeforeAsync(int year, CancellationToken cancellationToken);
        Task<List<AuthorDto>> GetAuthorsBornAfterAsync(int year, CancellationToken cancellationToken);

        Task<List<BookDto>> GetAllBooksAsync(CancellationToken cancellationToken);
        Task<BookDetailsDto?> GetBookByIdAsync(int id, CancellationToken cancellationToken);
        Task<Book> CreateBookAsync(Book book, CancellationToken cancellationToken);
        Task<bool> UpdateBookAsync(Book book, CancellationToken cancellationToken);
        Task<bool> DeleteBookAsync(int id, CancellationToken cancellationToken);
        Task<List<BookDto>> GetBooksPublishedBeforeAsync(int year, CancellationToken cancellationToken);
        Task<List<BookDto>> GetBooksPublishedAfterAsync(int year, CancellationToken cancellationToken); 
    }
}

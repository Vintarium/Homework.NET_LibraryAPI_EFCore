using Homework.NET_LibraryAPI.Models.DTO;

namespace Homework.NET_LibraryAPI.Services.Interfaces
{
    public interface IBookService
    {
        Task<List<BookDto>> GetAllBooksAsync(CancellationToken cancellationToken);
        Task<BookDetailsDto?> GetBookByIdAsync(int id, CancellationToken cancellationToken);
        Task<List<BookDto>> GetBooksPublishedAfterAsync(int year, CancellationToken cancellationToken);
        Task<List<BookDto>> GetBooksPublishedBeforeAsync(int year, CancellationToken cancellationToken);
        Task<BookDetailsDto> CreateBookAsync(BookCreationDto bookDto, CancellationToken cancellationToken);
        Task<bool> UpdateBookAsync(int id, BookUpdateDto bookDto, CancellationToken cancellationToken);
        Task<bool> DeleteBookAsync(int id, CancellationToken cancellationToken);
    }
}

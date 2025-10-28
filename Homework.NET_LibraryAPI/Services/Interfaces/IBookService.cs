using Homework.NET_LibraryAPI.Models.DTO;

namespace Homework.NET_LibraryAPI.Services.Interfaces
{
    public interface IBookService
    {
        IEnumerable<BookDto> GetAllBooks();
        BookDetailsDto? GetBookById(int id);
        IEnumerable<BookDto> GetBooksPublishedAfter(int year);
        IEnumerable<BookDto> GetBooksPublishedBefore(int year);
        BookDetailsDto CreateBook(BookCreationDto bookDto);
        bool UpdateBook(int id, BookUpdateDto bookDto);
        bool DeleteBook(int id);
    }
}

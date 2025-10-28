using Homework.NET_LibraryAPI.Models;
using Homework.NET_LibraryAPI.Models.DTO;

namespace Homework.NET_LibraryAPI.Repositories.Interfaces
{
    public interface ILibraryRepository
    {
        List<AuthorDto> GetAllAuthors();
        AuthorDetailsDto? GetAuthorById(int id);
        Author CreateAuthor(Author author);
        bool UpdateAuthor(Author author);
        bool DeleteAuthor(int id);
        List<AuthorDto> GetAuthorsBornBefore(int year);
        List<AuthorDto> GetAuthorsBornAfter(int year);

        List<BookDto> GetAllBooks();
        BookDetailsDto? GetBookById(int id);
        Book CreateBook(Book book);
        bool UpdateBook(Book book);
        bool DeleteBook(int id);
        List<BookDto> GetBooksPublishedBefore(int year);
        List<BookDto> GetBooksPublishedAfter(int year); 
    }
}

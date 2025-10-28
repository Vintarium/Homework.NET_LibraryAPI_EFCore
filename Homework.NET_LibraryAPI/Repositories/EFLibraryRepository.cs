using Homework.NET_LibraryAPI.Data;
using Homework.NET_LibraryAPI.Models;
using Homework.NET_LibraryAPI.Models.DTO;
using Homework.NET_LibraryAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Homework.NET_LibraryAPI.Repositories
{
    public class EFLibraryRepository : ILibraryRepository
    {
        private readonly LibraryContext _context;
        public EFLibraryRepository(LibraryContext context)
        {
            _context = context;
        }
        public Author CreateAuthor(Author author)
        {
            _context.Authors.Add(author);
            _context.SaveChanges();
            return author;
        }
        public List<AuthorDto> GetAllAuthors()
        {
            return _context.Authors
                           .Select(a => new AuthorDto
                           {
                               Id = a.Id,
                               Name = $"{a.Name} ( Количество книг этого автора в нашей библиотеке: {a.Books.Count} )",
                               DateOfBirth = a.DateOfBirth,
                               Books = a.Books.Select(b => new BookDto
                               {
                                   Id = b.Id,
                                   Title = b.Title,
                                   PublishedYear = b.PublishedYear,
                                   AuthorId = b.AuthorId
                               }).ToList()
                           })
                           .ToList();
        }
        public AuthorDetailsDto? GetAuthorById(int id)
        {
            return _context.Authors
                           .Where(a => a.Id == id)
                           .Select(a => new AuthorDetailsDto
                           {
                               Id = a.Id,
                               Name = a.Name,
                               DateOfBirth = a.DateOfBirth,
                               Books = a.Books.Select(b => new BookDto
                               {
                                   Id = b.Id,
                                   Title = b.Title,
                                   PublishedYear = b.PublishedYear,
                                   AuthorId = b.AuthorId
                               }).ToList()
                           })
                           .FirstOrDefault();
        }

        public bool UpdateAuthor(Author author)
        {
            var existsAuthor = _context.Authors.FirstOrDefault(a => a.Id == author.Id);
            if (existsAuthor != null)
            {
                existsAuthor.Name = author.Name;
                existsAuthor.DateOfBirth = author.DateOfBirth;
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        public bool DeleteAuthor(int id)
        {
            var author = _context.Authors.FirstOrDefault(a => a.Id == id);
            if (author != null)
            {
                _context.Authors.Remove(author);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        public List<AuthorDto> GetAuthorsBornBefore(int year)
        {
            return _context.Authors
                           .Where(a => a.DateOfBirth < year)
                           .OrderBy(a => a.DateOfBirth)
                           .Select(a => new AuthorDto { Id = a.Id, Name = a.Name, DateOfBirth = a.DateOfBirth })
                           .ToList();
        }
        public List<AuthorDto> GetAuthorsBornAfter(int year)
        {
            return _context.Authors
                           .Where(a => a.DateOfBirth > year)
                           .OrderBy(a => a.DateOfBirth)
                           .Select(a => new AuthorDto { Id = a.Id, Name = a.Name, DateOfBirth = a.DateOfBirth })
                           .ToList();
        }
        public Book CreateBook(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
            return book;
        }

        public List<BookDto> GetAllBooks()
        {
            return _context.Books
                           .OrderBy(b => b.Id)
                           .Select(b => new BookDto { Id = b.Id, Title = b.Title, PublishedYear = b.PublishedYear, AuthorId = b.AuthorId })
                           .ToList();
        }
        public BookDetailsDto? GetBookById(int id)
        {
            return _context.Books
                           .Where(b => b.Id == id)
                           .Select(b => new BookDetailsDto
                           {
                               Id = b.Id,
                               Title = b.Title,
                               PublishedYear = b.PublishedYear,
                               AuthorId = b.AuthorId,
                               Author = new AuthorNestedDto
                               {
                                   Id = b.Author!.Id,
                                   Name = b.Author!.Name,
                                   DateOfBirth = b.Author!.DateOfBirth,
                               }
                           })
                           .FirstOrDefault();
        }
        public bool UpdateBook(Book book)
        {
            var existingBook = _context.Books.FirstOrDefault(b => b.Id == book.Id);

            if (existingBook != null)
            {
                existingBook.Title = book.Title;
                existingBook.PublishedYear = book.PublishedYear;
                existingBook.AuthorId = book.AuthorId;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool DeleteBook(int id)
        {
            var book = _context.Books.FirstOrDefault(b => b.Id == id);
            if (book != null)
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        public List<BookDto> GetBooksPublishedAfter(int year)
        {
            return _context.Books
                           .Where(b => b.PublishedYear > year)
                           .OrderBy(b => b.PublishedYear)
                           .Select(b => new BookDto { Id = b.Id, Title = b.Title, PublishedYear = b.PublishedYear, AuthorId = b.AuthorId })
                           .ToList();
        }
        public List<BookDto> GetBooksPublishedBefore(int year)
        {
            return _context.Books
                           .Where(b => b.PublishedYear < year)
                           .OrderBy(b => b.PublishedYear)
                           .Select(b => new BookDto { Id = b.Id, Title = b.Title, PublishedYear = b.PublishedYear, AuthorId = b.AuthorId })
                           .ToList();
        }
    }
}
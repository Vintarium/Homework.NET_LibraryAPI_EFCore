using Homework.NET_LibraryAPI.Data;
using Homework.NET_LibraryAPI.Models;
using Homework.NET_LibraryAPI.Models.DTO;
using Homework.NET_LibraryAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace Homework.NET_LibraryAPI.Repositories
{
    public class EFLibraryRepository : ILibraryRepository
    {
        private readonly LibraryContext _context;
        public EFLibraryRepository(LibraryContext context)
        {
            _context = context;
        }
        public async Task<Author> CreateAuthorAsync(Author author, CancellationToken cancellationToken)
        {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync(cancellationToken);
            return author;
        }
        public async Task<List<AuthorDto>> GetAllAuthorsAsync(CancellationToken cancellationToken)
        {
            return await _context.Authors
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
                           .ToListAsync(cancellationToken);
        }
        public async Task<AuthorDetailsDto?> GetAuthorByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Authors
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
                           .FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateAuthorAsync(Author author, CancellationToken cancellationToken)
        {
            var existsAuthor = await _context.Authors.FirstOrDefaultAsync(a => a.Id == author.Id);
            if (existsAuthor != null)
            {
                existsAuthor.Name = author.Name;
                existsAuthor.DateOfBirth = author.DateOfBirth;
                await _context.SaveChangesAsync(cancellationToken);
                return true;
            }
            return false;
        }
        public async Task<bool> DeleteAuthorAsync(int id, CancellationToken cancellationToken)
        {
            var author = await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);
            if (author != null)
            {
                _context.Authors.Remove(author);
             await   _context.SaveChangesAsync(cancellationToken);
                return true;
            }
            return false;
        }
        public async Task<List<AuthorDto>> GetAuthorsBornBeforeAsync(int year, CancellationToken cancellationToken)
        {
            return await _context.Authors
                           .Where(a => a.DateOfBirth < year)
                           .OrderBy(a => a.DateOfBirth)
                           .Select(a => new AuthorDto { Id = a.Id, Name = a.Name, DateOfBirth = a.DateOfBirth })
                           .ToListAsync();
        }
        public async Task<List<AuthorDto>> GetAuthorsBornAfterAsync(int year, CancellationToken cancellationToken)
        {
            return await _context.Authors
                           .Where(a => a.DateOfBirth > year)
                           .OrderBy(a => a.DateOfBirth)
                           .Select(a => new AuthorDto { Id = a.Id, Name = a.Name, DateOfBirth = a.DateOfBirth })
                           .ToListAsync();
        }
        public async Task<Book> CreateBookAsync(Book book, CancellationToken cancellationToken)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync(cancellationToken);
            return book;
        }

        public async Task<List<BookDto>> GetAllBooksAsync(CancellationToken cancellationToken)
        {
            return await _context.Books
                           .OrderBy(b => b.Id)
                           .Select(b => new BookDto { Id = b.Id, Title = b.Title, PublishedYear = b.PublishedYear, AuthorId = b.AuthorId })
                           .ToListAsync();
        }
        public async Task<BookDetailsDto?> GetBookByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Books
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
                           .FirstOrDefaultAsync();
        }
        public async Task<bool> UpdateBookAsync(Book book, CancellationToken cancellationToken)
        {
            var existingBook = await _context.Books.FirstOrDefaultAsync(b => b.Id == book.Id);

            if (existingBook != null)
            {
                existingBook.Title = book.Title;
                existingBook.PublishedYear = book.PublishedYear;
                existingBook.AuthorId = book.AuthorId;
                await _context.SaveChangesAsync(cancellationToken);
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteBookAsync(int id, CancellationToken cancellationToken)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync(cancellationToken);
                return true;
            }
            return false;
        }
        public async Task<List<BookDto>> GetBooksPublishedAfterAsync(int year, CancellationToken cancellationToken)
        {
            return await _context.Books
                           .Where(b => b.PublishedYear > year)
                           .OrderBy(b => b.PublishedYear)
                           .Select(b => new BookDto { Id = b.Id, Title = b.Title, PublishedYear = b.PublishedYear, AuthorId = b.AuthorId })
                           .ToListAsync();
        }
        public async Task<List<BookDto>> GetBooksPublishedBeforeAsync(int year, CancellationToken cancellationToken)
        {
            return await _context.Books
                           .Where(b => b.PublishedYear < year)
                           .OrderBy(b => b.PublishedYear)
                           .Select(b => new BookDto { Id = b.Id, Title = b.Title, PublishedYear = b.PublishedYear, AuthorId = b.AuthorId })
                           .ToListAsync();
        }
    }
}
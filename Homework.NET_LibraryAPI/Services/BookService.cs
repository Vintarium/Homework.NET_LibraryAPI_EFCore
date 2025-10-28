using Homework.NET_LibraryAPI.Models;
using Homework.NET_LibraryAPI.Models.DTO;
using Homework.NET_LibraryAPI.Repositories.Interfaces;
using Homework.NET_LibraryAPI.Services.Interfaces;
using System.Collections.Generic;

namespace Homework.NET_LibraryAPI.Services
{
    public class BookService : IBookService
    {
        private readonly ILibraryRepository _repo;
        public BookService(ILibraryRepository repo)
        {
            _repo = repo;
        }
        public IEnumerable<BookDto> GetAllBooks() => _repo.GetAllBooks();
        public BookDetailsDto? GetBookById(int id) => _repo.GetBookById(id);
        public IEnumerable<BookDto> GetBooksPublishedAfter(int year) => _repo.GetBooksPublishedAfter(year);
        public IEnumerable<BookDto> GetBooksPublishedBefore(int year) => _repo.GetBooksPublishedBefore(year);

        public BookDetailsDto CreateBook(BookCreationDto bookDto)
        {
            if (_repo.GetAuthorById(bookDto.AuthorId) == null)
            {
                return null!; 
            }
            var bookModel = new Book
            {
                Title = bookDto.Title,
                PublishedYear = bookDto.PublishedYear,
                AuthorId = bookDto.AuthorId
            };

            var createdBook = _repo.CreateBook(bookModel);
            return GetBookById(createdBook.Id)!;
        }

        public bool UpdateBook(int id, BookUpdateDto bookDto)
        {
            if (_repo.GetBookById(id) == null)
            {
                return false;
            }
            if (_repo.GetAuthorById(bookDto.AuthorId) == null)
            {
                return false;
            }
            var bookModel = new Book
            {
                Id = id,
                Title = bookDto.Title,
                PublishedYear = bookDto.PublishedYear,
                AuthorId = bookDto.AuthorId
            };
            return _repo.UpdateBook(bookModel);
        }

        public bool DeleteBook(int id)
        {
            return _repo.DeleteBook(id);
        }
    }
}
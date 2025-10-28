using Homework.NET_LibraryAPI.Models.DTO;
using Homework.NET_LibraryAPI.Models;
using Homework.NET_LibraryAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Homework.NET_LibraryAPI.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BooksController : ControllerBase
    {
        private readonly ILibraryRepository _repo;

        public BooksController(ILibraryRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult GetAllBooks()
        {
            return Ok(_repo.GetAllBooks());
        }

        [HttpGet("publishedafter/{year}")]
        public IActionResult GetBooksPublishedAfter(int year)
        {
            var books = _repo.GetBooksPublishedAfter(year);
            if (books.Any())
            {
                return Ok(books);
            }
            return NotFound($"Книги, опубликованные после {year} года, не найдены.");
        }

        [HttpGet("publishedbefore/{year}")]
        public IActionResult GetBooksPublishedBefore(int year)
        {
            var books = _repo.GetBooksPublishedBefore(year);
            if (books.Any())
            {
                return Ok(books);
            }
            return NotFound($"Книги, опубликованные до {year} года, не найдены.");
        }

        [HttpGet("{id}")]
        public IActionResult GetBookById(int id)
        {
            var book = _repo.GetBookById(id);
            if (book == null) return NotFound();
            return Ok(book);
        }

        [HttpPost]
        public IActionResult AddBook(BookCreationDto bookDto)
        {
            if (_repo.GetAuthorById(bookDto.AuthorId) == null)
            {
                return BadRequest($"Автора с ID {bookDto.AuthorId} не существует.");
            }
            var book = new Book
            {
                Title = bookDto.Title,
                PublishedYear = bookDto.PublishedYear,
                AuthorId = bookDto.AuthorId
            };

            var createdBook = _repo.CreateBook(book);
            return CreatedAtAction(nameof(GetBookById), new { id = createdBook.Id }, createdBook);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, BookUpdateDto bookDto)
        {
            if (_repo.GetBookById(id) == null)
            {
                return NotFound($"Книги с ID: {id} не существует");
            }
            if (_repo.GetAuthorById(bookDto.AuthorId) == null)
            {
                return BadRequest($"Автора с ID {bookDto.AuthorId} не существует.");
            }
            var bookModel = new Book
            {
                Id = id,
                Title = bookDto.Title,
                PublishedYear = bookDto.PublishedYear,
                AuthorId = bookDto.AuthorId
            };

            _repo.UpdateBook(bookModel);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var bookExists = _repo.DeleteBook(id);
            if (bookExists)
            {
                return NoContent();
            }
            return NotFound();
        }
    }
}
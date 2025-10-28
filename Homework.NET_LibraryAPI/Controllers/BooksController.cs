using Homework.NET_LibraryAPI.Models;
using Homework.NET_LibraryAPI.Models.DTO;
using Homework.NET_LibraryAPI.Repositories.Interfaces;
using Homework.NET_LibraryAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Homework.NET_LibraryAPI.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _service;
        public BooksController(IBookService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAllBooks()
        {
            return Ok(_service.GetAllBooks());
        }

        [HttpGet("publishedafter/{year}")]
        public IActionResult GetBooksPublishedAfter(int year)
        {
            var books = _service.GetBooksPublishedAfter(year);
            if (books.Any())
            {
                return Ok(books);
            }
            return NotFound($"Книги, опубликованные после {year} года, не найдены.");
        }

        [HttpGet("publishedbefore/{year}")]
        public IActionResult GetBooksPublishedBefore(int year)
        {
            var books = _service.GetBooksPublishedBefore(year);
            if (books.Any())
            {
                return Ok(books);
            }
            return NotFound($"Книги, опубликованные до {year} года, не найдены.");
        }

        [HttpGet("{id}")]
        public IActionResult GetBookById(int id)
        {
            var book = _service.GetBookById(id);
            if (book == null) return NotFound();
            return Ok(book);
        }

        [HttpPost]
        public IActionResult AddBook(BookCreationDto bookDto)
        {
            var createdBookDto = _service.CreateBook(bookDto);
            if (createdBookDto == null)
            {
                return BadRequest($"Автора с ID {bookDto.AuthorId} не существует.");
            }
            return CreatedAtAction(nameof(GetBookById), new { id = createdBookDto.Id }, createdBookDto);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, BookUpdateDto bookDto)
        {
            if (_service.UpdateBook(id, bookDto))
            {
                return NoContent();
            }

            if (_service.GetBookById(id) == null)
            {
                return NotFound($"Книги с ID: {id} не существует");
            }
            return BadRequest($"Автора с ID {bookDto.AuthorId} не существует.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            if (_service.DeleteBook(id))
            {
                return NoContent();
            }
            return NotFound();
        }
    }
}
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
        public async Task<IActionResult> GetAllBooks(CancellationToken cancellationToken)
        {
            return Ok(await _service.GetAllBooksAsync(cancellationToken));
        }

        [HttpGet("publishedafter/{year}")]
        public async Task<IActionResult> GetBooksPublishedAfter(int year, CancellationToken cancellationToken)
        {
            var books = await _service.GetBooksPublishedAfterAsync(year, cancellationToken);
            if (books.Any())
            {
                return Ok(books);
            }
            return NotFound($"Книги, опубликованные после {year} года, не найдены.");
        }

        [HttpGet("publishedbefore/{year}")]
        public async Task<IActionResult> GetBooksPublishedBefore(int year, CancellationToken cancellationToken)
        {
            var books = await _service.GetBooksPublishedBeforeAsync(year, cancellationToken);
            if (books.Any())
            {
                return Ok(books);
            }
            return NotFound($"Книги, опубликованные до {year} года, не найдены.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id, CancellationToken cancellationToken)
        {
            var book = await _service.GetBookByIdAsync(id, cancellationToken);
            if (book == null) return NotFound();
            return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> AddBook(BookCreationDto bookDto, CancellationToken cancellationToken)
        {
            var createdBookDto = await _service.CreateBookAsync(bookDto, cancellationToken);
            if (createdBookDto == null)
            {
                return BadRequest($"Автора с ID {bookDto.AuthorId} не существует.");
            }
            return CreatedAtAction(nameof(GetBookById), new { id = createdBookDto.Id }, createdBookDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, BookUpdateDto bookDto, CancellationToken cancellationToken)
        {
            if (await _service.UpdateBookAsync(id, bookDto, cancellationToken))
            {
                return NoContent();
            }

            if (await _service.GetBookByIdAsync(id, cancellationToken) == null)
            {
                return NotFound($"Книги с ID: {id} не существует");
            }
            return BadRequest($"Автора с ID {bookDto.AuthorId} не существует.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id, CancellationToken cancellationToken)
        {
            if (await _service.DeleteBookAsync(id, cancellationToken))
            {
                return NoContent();
            }
            return NotFound();
        }
    }
}
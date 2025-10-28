using Homework.NET_LibraryAPI.Models;
using Homework.NET_LibraryAPI.Models.DTO;
using Homework.NET_LibraryAPI.Repositories.Interfaces;
using Homework.NET_LibraryAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace Homework.NET_LibraryAPI.Controllers
{
    [ApiController]
    [Route("api/authors")]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _service;

        public AuthorsController(IAuthorService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAllAuthors()
        {
            return Ok(_service.GetAllAuthors());
        }

        [HttpGet("{id}")]
        public IActionResult GetAuthorById(int id)
        {
            var author = _service.GetAuthorById(id);
            if (author != null)
            {
                return Ok(author);
            }
            return NotFound($"Автора с ID: {id} не существует");
        }

        [HttpGet("bornbefore/{year}")]
        public IActionResult GetAuthorsBornBefore(int year)
        {
            var authors = _service.GetAuthorsBornBefore(year);
            if (authors.Any())
            {
                return Ok(authors);
            }
            return NotFound($"Авторы, родившиеся до {year} года, не найдены.");
        }

        [HttpGet("bornafter/{year}")]
        public IActionResult GetAuthorsBornAfter(int year)
        {
            var authors = _service.GetAuthorsBornAfter(year);
            if (authors.Any())
            {
                return Ok(authors);
            }
            return NotFound($"Авторы, родившиеся после {year} года, не найдены.");
        }

        [HttpPost]
        public IActionResult AddAuthor(AuthorCreationDto authorDto)
        {
            var createdAuthorDto = _service.CreateAuthor(authorDto);
            return CreatedAtAction(nameof(GetAuthorById), new { id = createdAuthorDto.Id }, createdAuthorDto);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAuthor(int id, AuthorUpdateDto authorDto)
        {
            if (_service.UpdateAuthor(id, authorDto))
            {
                return NoContent();
            }
            return NotFound($"Автора с ID: {id} не существует");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAuthor(int id)
        {
            if (_service.DeleteAuthor(id))
            {
                return NoContent();
            }
            return NotFound($"Автора с ID: {id} не существует");
        }
    }
}
using Homework.NET_LibraryAPI.Models.DTO;
using Homework.NET_LibraryAPI.Models;
using Homework.NET_LibraryAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace Homework.NET_LibraryAPI.Controllers
{
    [ApiController]
    [Route("api/authors")]
    public class AuthorsController : ControllerBase
    {
        private readonly ILibraryRepository _repo;
        public AuthorsController(ILibraryRepository repository)
        {
            _repo = repository;
        }

        [HttpGet]
        public IActionResult GetAllAuthors()
        {
            return Ok(_repo.GetAllAuthors());
        }

        [HttpGet("{id}")]
        public IActionResult GetAuthorById(int id)
        {
            var author = _repo.GetAuthorById(id);
            if (author != null)
            {
                return Ok(author);
            }
            return NotFound($"Автора с ID: {id} не существует");
        }

        [HttpGet("bornbefore/{year}")]
        public IActionResult GetAuthorsBornBefore(int year)
        {
            var authors = _repo.GetAuthorsBornBefore(year);
            if (authors.Any())
            {
                return Ok(authors);
            }
            return NotFound($"Авторы, родившиеся до {year} года, не найдены.");
        }

        [HttpGet("bornafter/{year}")]
        public IActionResult GetAuthorsBornAfter(int year)
        {
            var authors = _repo.GetAuthorsBornAfter(year);
            if (authors.Any())
            {
                return Ok(authors);
            }
            return NotFound($"Авторы, родившиеся после {year} года, не найдены.");
        }

        [HttpPost]
        public IActionResult AddAuthor(AuthorCreationDto authorDto)
        {
            var author = new Author
            {
                Name = authorDto.Name,
                DateOfBirth = authorDto.DateOfBirth
            };
            var createdAuthor = _repo.CreateAuthor(author);
            return CreatedAtAction(nameof(GetAuthorById), new { id = createdAuthor.Id }, createdAuthor);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAuthor(int id, AuthorUpdateDto authorDto)
        {
            if (_repo.GetAuthorById(id) == null)
            {
                return NotFound($"Автора с ID: {id} не существует");
            }

            var authorModel = new Author
            {
                Id = id,
                Name = authorDto.Name,
                DateOfBirth = authorDto.DateOfBirth
            };
            _repo.UpdateAuthor(authorModel);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAuthor(int id)
        {
            if (_repo.DeleteAuthor(id))
            {
                return NoContent();
            }
            return NotFound($"Автора с ID: {id} не существует");
        }
    }
}
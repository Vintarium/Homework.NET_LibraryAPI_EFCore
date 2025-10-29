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
        public async Task<IActionResult> GetAllAuthors(CancellationToken cancellationToken)
        {
            return Ok(await _service.GetAllAuthorsAsync(cancellationToken));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthorById(int id, CancellationToken cancellationToken)
        {
            var author = await _service.GetAuthorByIdAsync(id, cancellationToken);
            if (author != null)
            {
                return Ok(author);
            }
            return NotFound($"Автора с ID: {id} не существует");
        }

        [HttpGet("bornbefore/{year}")]
        public async Task<IActionResult> GetAuthorsBornBefore(int year, CancellationToken cancellationToken)
        {
            var authors = await _service.GetAuthorsBornBeforeAsync(year, cancellationToken);
            if (authors.Any())
            {
                return Ok(authors);
            }
            return NotFound($"Авторы, родившиеся до {year} года, не найдены.");
        }

        [HttpGet("bornafter/{year}")]
        public async Task<IActionResult> GetAuthorsBornAfter(int year, CancellationToken cancellationToken)
        {
            var authors = await _service.GetAuthorsBornAfterAsync(year, cancellationToken);
            if (authors.Any())
            {
                return Ok(authors);
            }
            return NotFound($"Авторы, родившиеся после {year} года, не найдены.");
        }

        [HttpPost]
        public async Task<IActionResult> AddAuthor(AuthorCreationDto authorDto, CancellationToken cancellationToken)
        {
            var createdAuthorDto = await _service.CreateAuthorAsync(authorDto, cancellationToken);
            return CreatedAtAction(nameof(GetAuthorById), new { id = createdAuthorDto.Id }, createdAuthorDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, AuthorUpdateDto authorDto, CancellationToken cancellationToken)
        {
            if (await _service.UpdateAuthorAsync(id, authorDto, cancellationToken))
            {
                return NoContent();
            }
            return NotFound($"Автора с ID: {id} не существует");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id, CancellationToken cancellationToken)
        {
            if (await _service.DeleteAuthorAsync(id, cancellationToken))
            {
                return NoContent();
            }
            return NotFound($"Автора с ID: {id} не существует");
        }
    }
}
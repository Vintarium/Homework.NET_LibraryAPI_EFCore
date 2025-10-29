using Homework.NET_LibraryAPI.Models;
using Homework.NET_LibraryAPI.Models.DTO;
using Homework.NET_LibraryAPI.Repositories.Interfaces;
using Homework.NET_LibraryAPI.Services.Interfaces;
using System.Collections.Generic;
using System.Threading;

namespace Homework.NET_LibraryAPI.Services
{
    public class BookService : IBookService
    {
        private readonly ILibraryRepository _repo;
        public BookService(ILibraryRepository repo)
        {
            _repo = repo;
        }
        public async Task<List<BookDto>> GetAllBooksAsync(CancellationToken cancellationToken) =>
            await _repo.GetAllBooksAsync(cancellationToken);
        public async Task<BookDetailsDto?> GetBookByIdAsync(int id, CancellationToken cancellationToken) =>
            await _repo.GetBookByIdAsync(id,cancellationToken);
        public async Task<List<BookDto>> GetBooksPublishedAfterAsync(int year, CancellationToken cancellationToken) =>
            await _repo.GetBooksPublishedAfterAsync(year,cancellationToken);
        public async Task<List<BookDto>> GetBooksPublishedBeforeAsync(int year, CancellationToken cancellationToken) =>
            await _repo.GetBooksPublishedBeforeAsync(year, cancellationToken);

        public async Task<BookDetailsDto> CreateBookAsync(BookCreationDto bookDto, CancellationToken cancellationToken)
        {
            if (await _repo.GetAuthorByIdAsync(bookDto.AuthorId, cancellationToken) == null)
            {
                return null!; 
            }
            var bookModel = new Book
            {
                Title = bookDto.Title,
                PublishedYear = bookDto.PublishedYear,
                AuthorId = bookDto.AuthorId
            };

            var createdBook =  await _repo.CreateBookAsync(bookModel, cancellationToken);
            return (await GetBookByIdAsync(createdBook.Id, cancellationToken))!;
        }

        public async Task<bool> UpdateBookAsync(int id, BookUpdateDto bookDto,CancellationToken cancellationToken)
        {
            if (await _repo.GetBookByIdAsync(id, cancellationToken) == null)
            {
                return false;
            }
            if (await _repo.GetAuthorByIdAsync(bookDto.AuthorId, cancellationToken) == null)
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
            return await _repo.UpdateBookAsync(bookModel, cancellationToken);
        }

        public async Task<bool> DeleteBookAsync(int id, CancellationToken cancellationToken)
        {
            return await _repo.DeleteBookAsync(id, cancellationToken);
        }
    }
}
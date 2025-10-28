using System.ComponentModel.DataAnnotations;

namespace Homework.NET_LibraryAPI.Models.DTO
{
    public class BookCreationDto
    {
        [Required(ErrorMessage = "Название книги не может быть пустым.")]
        public string? Title { get; set; }

        [Range(-1000, 2025, ErrorMessage = "Год публикации должен быть реалистичным.")]
        public int PublishedYear { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "ID автора должен быть указан и быть больше 0.")]
        public int AuthorId { get; set; }
    }
}

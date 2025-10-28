using System.ComponentModel.DataAnnotations;

namespace Homework.NET_LibraryAPI.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Название книги не может быть пустым.")]
        public string ? Title { get; set; }

        [Range(-1000, 2025, ErrorMessage = "Год публикации должен быть реалистичным (например, от 1000 г. до н.э. до 2025 г. н.э.). Для более древней литературы мы создадим отдельный раздел.")]
        public int PublishedYear { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "ID автора должен быть указан и быть больше 0.")]
        public int AuthorId { get; set; }

        public Author? Author { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Homework.NET_LibraryAPI.Models
{
    public class Author
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Имя автора не может быть пустым.")]
        [StringLength(100)]
        public string ? Name { get; set; }

        [Range(-1000, 2025, ErrorMessage = "Год рождения должен быть реалистичным (например, от 1000 г. до н.э. до 2025 г. н.э.). Для более древней литературы мы создадим отдельный раздел.")]
        public int DateOfBirth { get; set; }
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}

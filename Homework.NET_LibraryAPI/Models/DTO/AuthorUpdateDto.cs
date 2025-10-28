using System.ComponentModel.DataAnnotations;

namespace Homework.NET_LibraryAPI.Models.DTO
{
    public class AuthorUpdateDto
    {
        [Required(ErrorMessage = "Имя автора не может быть пустым.")]
        [StringLength(100)]
        public string? Name { get; set; }

        [Range(-1000, 2025, ErrorMessage = "Год рождения должен быть реалистичным.")]
        public int DateOfBirth { get; set; }
    }
}

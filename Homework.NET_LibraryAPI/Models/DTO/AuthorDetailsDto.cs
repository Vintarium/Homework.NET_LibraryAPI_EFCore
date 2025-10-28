namespace Homework.NET_LibraryAPI.Models.DTO
{
    public class AuthorDetailsDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int DateOfBirth { get; set; }
        public ICollection<BookDto> Books { get; set; } = new List<BookDto>();
    }
}

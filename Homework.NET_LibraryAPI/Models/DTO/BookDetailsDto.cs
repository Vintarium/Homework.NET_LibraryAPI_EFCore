namespace Homework.NET_LibraryAPI.Models.DTO
{
    public class BookDetailsDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public int PublishedYear { get; set; }
        public int AuthorId { get; set; }
        public AuthorNestedDto? Author { get; set; }
    }
}

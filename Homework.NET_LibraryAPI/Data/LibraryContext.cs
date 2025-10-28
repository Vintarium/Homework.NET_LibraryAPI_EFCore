using Homework.NET_LibraryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Homework.NET_LibraryAPI.Data
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) { }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Author>().HasData(
                new Author { Id = 1, Name = "Лев Толстой", DateOfBirth = 1828 },
                new Author { Id = 2, Name = "Александр Пушкин", DateOfBirth = 1799 },
                new Author { Id = 3, Name = "Роберт С. Мартин (Дядюшка Боб)", DateOfBirth = 1952 },
                new Author { Id = 4, Name = "Джордж Оруэлл", DateOfBirth = 1903 },
                new Author { Id = 5, Name = "Федор Достоевский", DateOfBirth = 1821 },
                new Author { Id = 6, Name = "Артур Конан Дойль", DateOfBirth = 1859 },
                new Author { Id = 7, Name = "Джек Лондон", DateOfBirth = 1876 },
                new Author { Id = 8, Name = "Маргарет Этвуд", DateOfBirth = 1939 },
                new Author { Id = 9, Name = "Гомер", DateOfBirth = -750 },
                new Author { Id = 10, Name = "Платон", DateOfBirth = -428 },
                new Author { Id = 11, Name = "Рене Декарт", DateOfBirth = 1596 },
                new Author { Id = 12, Name = "Иммануил Кант", DateOfBirth = 1724 },
                new Author { Id = 13, Name = "Блаженный Августин", DateOfBirth = 354 },
                new Author { Id = 14, Name = "Василий Великий", DateOfBirth = 329 },
                new Author { Id = 15, Name = "Нестор Летописец", DateOfBirth = 1056 }
            );

            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, Title = "Война и мир", PublishedYear = 1869, AuthorId = 1 },
                new Book { Id = 2, Title = "Анна Каренина", PublishedYear = 1877, AuthorId = 1 },
                new Book { Id = 3, Title = "Евгений Онегин", PublishedYear = 1833, AuthorId = 2 },
                new Book { Id = 4, Title = "Чистый код", PublishedYear = 2008, AuthorId = 3 },
                new Book { Id = 5, Title = "1984", PublishedYear = 1949, AuthorId = 4 },
                new Book { Id = 6, Title = "Скотный двор", PublishedYear = 1945, AuthorId = 4 },
                new Book { Id = 7, Title = "Преступление и наказание", PublishedYear = 1866, AuthorId = 5 },
                new Book { Id = 8, Title = "Идиот", PublishedYear = 1868, AuthorId = 5 },
                new Book { Id = 9, Title = "Этюд в багровых тонах", PublishedYear = 1887, AuthorId = 6 },
                new Book { Id = 10, Title = "Собака Баскервилей", PublishedYear = 1902, AuthorId = 6 },
                new Book { Id = 11, Title = "Мартин Иден", PublishedYear = 1909, AuthorId = 7 },
                new Book { Id = 12, Title = "Белый Клык", PublishedYear = 1906, AuthorId = 7 },
                new Book { Id = 13, Title = "Рассказ служанки", PublishedYear = 1985, AuthorId = 8 },
                new Book { Id = 14, Title = "Илиада", PublishedYear = -750, AuthorId = 9 },
                new Book { Id = 15, Title = "Одиссея", PublishedYear = -750, AuthorId = 9 },
                new Book { Id = 16, Title = "Государство", PublishedYear = -380, AuthorId = 10 },
                new Book { Id = 17, Title = "Дискорс о методе", PublishedYear = 1637, AuthorId = 11 },
                new Book { Id = 18, Title = "Критика чистого разума", PublishedYear = 1781, AuthorId = 12 },
                new Book { Id = 19, Title = "Исповедь", PublishedYear = 397, AuthorId = 13 },
                new Book { Id = 20, Title = "О граде Божием", PublishedYear = 426, AuthorId = 13 },
                new Book { Id = 21, Title = "Шестоднев", PublishedYear = 370, AuthorId = 14 },
                new Book { Id = 22, Title = "Повесть временных лет", PublishedYear = 1113, AuthorId = 15 },
                new Book { Id = 23, Title = "Слово о законе и благодати", PublishedYear = 1037, AuthorId = 15 }

            );
        }
    }
}
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Homework.NET_LibraryAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddMoreClassicalAuthors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "DateOfBirth", "Name" },
                values: new object[,]
                {
                    { 3, 1952, "Роберт С. Мартин (Дядюшка Боб)" },
                    { 4, 1903, "Джордж Оруэлл" },
                    { 5, 1821, "Федор Достоевский" },
                    { 6, 1859, "Артур Конан Дойль" },
                    { 7, 1876, "Джек Лондон" },
                    { 8, 1939, "Маргарет Этвуд" },
                    { 9, -750, "Гомер" },
                    { 10, -428, "Платон" },
                    { 11, 1596, "Рене Декарт" },
                    { 12, 1724, "Иммануил Кант" },
                    { 13, 354, "Блаженный Августин" },
                    { 14, 329, "Василий Великий" },
                    { 15, 1056, "Нестор Летописец" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "PublishedYear", "Title" },
                values: new object[,]
                {
                    { 4, 3, 2008, "Чистый код" },
                    { 5, 4, 1949, "1984" },
                    { 6, 4, 1945, "Скотный двор" },
                    { 7, 5, 1866, "Преступление и наказание" },
                    { 8, 5, 1868, "Идиот" },
                    { 9, 6, 1887, "Этюд в багровых тонах" },
                    { 10, 6, 1902, "Собака Баскервилей" },
                    { 11, 7, 1909, "Мартин Иден" },
                    { 12, 7, 1906, "Белый Клык" },
                    { 13, 8, 1985, "Рассказ служанки" },
                    { 14, 9, -750, "Илиада" },
                    { 15, 9, -750, "Одиссея" },
                    { 16, 10, -380, "Государство" },
                    { 17, 11, 1637, "Дискорс о методе" },
                    { 18, 12, 1781, "Критика чистого разума" },
                    { 19, 13, 397, "Исповедь" },
                    { 20, 13, 426, "О граде Божием" },
                    { 21, 14, 370, "Шестоднев" },
                    { 22, 15, 1113, "Повесть временных лет" },
                    { 23, 15, 1037, "Слово о законе и благодати" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 15);
        }
    }
}

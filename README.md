## Homework.NET_LibraryAPI

Проект ASP.NET Core Web API для управления библиотекой (Авторы и Книги).
Данные хранятся в памяти (`List<T>`) на время работы приложения.

---

### ⚙️ Структура проекта

* `/Controllers` - API контроллеры, "лицо" приложения.
    * `AuthorsController.cs` - CRUD-операции для авторов.
    * `BooksController.cs` - CRUD-операции для книг.
* `/Repositories` - Слой доступа к данным.
    * `InMemoryRepository.cs` - Реализация репозитория (хранит `List<T>`).
    * `ILibraryRepository.cs` - Интерфейс (контракт) репозитория.
* `/Models` - Модели данных с атрибутами валидации (`[Required]`, `[Range]`).
    * `Author.cs` - Модель автора.
    * `Book.cs` - Модель книги.
* `/Homework.NET_LibraryAPI.Tests` - Проект с Unit-тестами (xUnit).
    * `InMemoryRepositoryTests.cs` - Модульные тесты для репозитория.
* `Program.cs` - Настройка API: Dependency Injection, Swagger и "клей" всего приложения.

---

После запуска **Swagger UI** (веб-интерфейс) откроется автоматически и позволит протестировать API.


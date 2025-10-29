# 🚀 Homework.NET_LibraryAPI_EFCore (Async Ready)

Проект ASP.NET Core 8 Web API для управления библиотекой (Авторы и Книги). Проект демонстрирует переход к масштабируемой работе с БД через **Entity Framework Core**.

## 🎯 Основные Технологии и Архитектура

* Платформа: .NET 8 (ASP.NET Core Web API)
* **Асинхронность:** Полностью реализована сквозная модель **async/await** и **Task** во всех слоях (Controller, Service, Repository). Это устраняет блокировку потоков и повышает масштабируемость (I/O Bound).
* База данных: SQLite (через EF Core)
* Подход к данным: Code First (Миграции EF Core)
* **Архитектура:** **Трехслойная (Controller -> Service -> Repository)**, Dependency Injection (DI)
* Сериализация: Полностью внедрена архитектура **DTO (Data Transfer Objects)** для всех операций CRUD, что устраняет проблемы циклической сериализации и повышает чистоту API-контракта.

## 🛠️ Структура Проекта

- **Controllers:** AuthorsController.cs, BooksController.cs - Методы `async Task<IActionResult>` принимают DTO и **вызывают Слой Сервисов с await**.
- **Services:** IAuthorService.cs, IBookService.cs, AuthorService.cs, BookService.cs - **Слой Бизнес-Логики.** Вся логика, DTO-преобразования и проверки **полностью асинхронны**.
- **Repositories:** EFLibraryRepository.cs, ILibraryRepository.cs - Слой доступа к данным. Все методы используют **`*Async`** и **`CancellationToken`**.
- **Data:** LibraryContext.cs - Контекст базы данных EF Core.
- **Migrations:** Файлы миграций. Автоматически применяются при запуске (Database.Migrate()).
- **Models:** Author.cs, Book.cs - **Сущности предметной области (EF Entities).**
- **Models/DTO:** 7 DTO-классов - Обеспечивает чистый API-контракт.
- **Program.cs:** Файл настройки. Конфигурация DI (Репозиторий Scoped, Сервисы Scoped).
- **Homework.NET_LibraryAPI.Tests:** EFLibraryRepositoryTests.cs - Unit-тесты (xUnit), **обновлены до асинхронного формата** (`async Task`), проверяют корректность LINQ-фильтрации.

## Запуск Проекта

1. Склонируйте репозиторий.
2. Установите инструменты .NET.
3. **Запустите проект** (например, через Visual Studio (F5) или командой dotnet run).
4. Проект автоматически создаст файл базы данных Library.db и заполнит его начальными данными (Seed-данными).

## Ключевые Эндпоинты

- GET /api/authors - Получить список всех авторов (с количеством книг).
- GET /api/authors/{id} - Получить детальную информацию об авторе (включая список его книг).
- GET /api/authors/bornafter/{year} - Фильтрация: получить авторов, родившихся после указанного года.
- POST /api/authors - Создать нового автора (использует AuthorCreationDto).
- GET /api/books/{id} - Получить детальную информацию о книге (включая минимальные данные об авторе).
- GET /api/books/publishedafter/{year} - Фильтрация: получить книги, опубликованные после указанного года.

---
**Сразу после запуска откроется Swagger UI, который позволит вам протестировать все доступные API-эндпоинты.**

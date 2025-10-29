# Homework.NET_LibraryAPI_EFCore

Проект ASP.NET Core 8 Web API для управления библиотекой (Авторы и Книги). Проект демонстрирует переход от In-Memory хранения данных к полноценной работе с базой данных через **Entity Framework Core**.

## Основные Технологии и Архитектура

* Платформа: .NET 8 (ASP.NET Core Web API)
* База данных: SQLite (через EF Core)
* Подход к данным: Code First (Миграции EF Core)
* **Архитектура:** **Трехслойная (Controller -> Service -> Repository)**, Dependency Injection (DI)
* Сериализация: Полностью внедрена архитектура **DTO (Data Transfer Objects)** для всех операций CRUD, что устраняет проблемы циклической сериализации и повышает чистоту API-контракта.

## Структура Проекта

- **Controllers:** AuthorsController.cs, BooksController.cs - API-интерфейсы. **Вызывают Слой Сервисов.**
- **Services:** IAuthorService.cs, IBookService.cs, AuthorService.cs, BookService.cs - **Слой Бизнес-Логики.** Содержит DTO-преобразования и проверки (например, существование автора). **Вызывает Репозиторий.**
- **Repositories:** EFLibraryRepository.cs, ILibraryRepository.cs - Слой доступа к данным. Реализация использует **LINQ-проекции** для преобразования моделей EF Core в DTO. **Работает только с БД.**
- **Data:** LibraryContext.cs - Контекст базы данных EF Core. Настроены отношения **один-ко-многим** и Seed-данные.
- **Migrations:** Файлы миграций. Автоматически применяются при запуске (Database.Migrate()).
- **Models:** Author.cs, Book.cs - **Сущности предметной области (EF Entities).**
- **Models/DTO:** 7 DTO-классов - Обеспечивает чистый API-контракт.
- **Program.cs:** Файл настройки. Конфигурация DI (Репозиторий Scoped, **Сервисы Scoped**), подключение LibraryContext, настройка Swagger, автоприменение миграций.
- **Homework.NET_LibraryAPI.Tests:** EFLibraryRepositoryTests.cs - Unit-тесты (xUnit), проверяющие корректность LINQ-фильтрации и DTO-проекций.

## Запуск Проекта

1. Склонируйте репозиторий.
2. Установите инструменты .NET.
3. Запустите проект (например, через Visual Studio (F5) или командой dotnet run).
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

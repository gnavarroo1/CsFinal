# Library Management System

This is a simple C# console application for managing a small library. It allows you to:

- Add and remove books (up to 5 titles)
- Show all books, indicating which are available and which are checked out
- Search for a book and see if it is available or checked out
- Borrow books (limit: 3 per user, only if available)
- Return books (select from the user's borrowed list)

## Features
- Each book can only be borrowed by one user at a time (stock = 1 per book)
- User-friendly menus and input validation
- All logic is separated into a service class for maintainability

## Usage
1. Run the application.
2. Use the menu to add, remove, show, search, borrow, or return books.
3. When borrowing or returning, you will be prompted for a username and to select a book by its number.

## Requirements
- .NET 6.0 or later

## Structure
- `Program.cs`: Main program and user interface logic
- `LibraryService.cs`: Service class with all library logic

## Example
```
Library Management
1. Add book
2. Remove book
3. Show books
4. Search book
5. Borrow book
6. Return book
7. Exit
Select an option (1-7):
```

## License
MIT

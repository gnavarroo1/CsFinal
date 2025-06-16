using System;
namespace CsFinal
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var library = new LibraryService();
            while (true)
            {
                ShowMenu();
                string? option = Console.ReadLine();
                switch (option)
                {
                    case "1": AddBookView(library); break;
                    case "2": RemoveBookView(library); break;
                    case "3": ShowBooksView(library); break;
                    case "4": SearchBookView(library); break;
                    case "5": BorrowBookView(library); break;
                    case "6": ReturnBookView(library); break;
                    case "7":
                        Console.WriteLine("Exiting the program...");
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }
        static void ShowMenu()
        {
            Console.WriteLine("\nLibrary Management");
            Console.WriteLine("1. Add book");
            Console.WriteLine("2. Remove book");
            Console.WriteLine("3. Show books");
            Console.WriteLine("4. Search book");
            Console.WriteLine("5. Borrow book");
            Console.WriteLine("6. Return book");
            Console.WriteLine("7. Exit");
            Console.Write("Select an option (1-7): ");
        }
        static void AddBookView(LibraryService library)
        {
            if (library.IsFull())
            {
                Console.WriteLine("No more space for books.");
                return;
            }
            Console.Write("Enter the title of the book to add: ");
            string? newBook = Console.ReadLine();
            if (!library.AddBook(newBook))
                Console.WriteLine("Could not add the book (may be empty or already exists).");
            else
                Console.WriteLine($"Book '{newBook}' added.");
        }
        static void RemoveBookView(LibraryService library)
        {
            if (library.IsEmpty())
            {
                Console.WriteLine("No books to remove.");
                return;
            }
            Console.Write("Enter the title of the book to remove: ");
            string? removeBook = Console.ReadLine();
            if (library.RemoveBook(removeBook))
                Console.WriteLine($"Book '{removeBook}' removed.");
            else
                Console.WriteLine("Book not found in the library.");
        }
        static void ShowBooksView(LibraryService library)
        {
            var books = library.GetBooks();
            Console.WriteLine("\nBooks in the library:");
            if (books.Length == 0)
                Console.WriteLine("(No books in the library)");
            else
                for (int i = 0; i < books.Length; i++)
                {
                    string status = library.IsBookCheckedOut(books[i]) ? "[Checked out]" : "[Available]";
                    Console.WriteLine($"{i + 1}. {books[i]} {status}");
                }
        }
        static void SearchBookView(LibraryService library)
        {
            Console.Write("Enter the title of the book to search: ");
            string? searchBook = Console.ReadLine();
            if (library.SearchBook(searchBook))
            {
                if (library.IsBookCheckedOut(searchBook))
                    Console.WriteLine($"The book '{searchBook}' is currently checked out.");
                else
                    Console.WriteLine($"The book '{searchBook}' is available in the library.");
            }
            else
                Console.WriteLine($"The book '{searchBook}' is not in the library.");
        }
        static void BorrowBookView(LibraryService library)
        {
            Console.Write("Enter the username requesting the loan: ");
            string? userBorrow = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(userBorrow))
            {
                Console.WriteLine("You must enter a valid username.");
                return;
            }
            if (library.BorrowedCount(userBorrow) >= 3)
            {
                Console.WriteLine("This user already has the maximum of 3 borrowed books.");
                return;
            }
            var availableBooks = new List<string>();
            var allBooks = library.GetBooks();
            for (int i = 0; i < allBooks.Length; i++)
            {
                if (!library.IsBookCheckedOut(allBooks[i]))
                    availableBooks.Add(allBooks[i]);
            }
            if (availableBooks.Count == 0)
            {
                Console.WriteLine("No books available to borrow.");
                return;
            }
            Console.WriteLine("Available books to borrow:");
            for (int i = 0; i < availableBooks.Count; i++)
                Console.WriteLine($"{i + 1}. {availableBooks[i]}");
            Console.Write("Enter the number of the book to borrow: ");
            string? inputIndex = Console.ReadLine();
            if (!int.TryParse(inputIndex, out int bookIndex) || bookIndex < 1 || bookIndex > availableBooks.Count)
            {
                Console.WriteLine("Invalid selection.");
                return;
            }
            string bookToBorrow = availableBooks[bookIndex - 1];
            if (library.BorrowBook(userBorrow, bookToBorrow))
                Console.WriteLine($"{userBorrow} has borrowed the book '{bookToBorrow}'.");
            else
                Console.WriteLine("Could not borrow the book.");
        }
        static void ReturnBookView(LibraryService library)
        {
            Console.Write("Enter the username returning the book: ");
            string? userReturn = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(userReturn))
            {
                Console.WriteLine("You must enter a valid username.");
                return;
            }
            var borrowed = library.GetBorrowedBooks(userReturn);
            if (borrowed.Count == 0)
            {
                Console.WriteLine($"{userReturn} has no borrowed books.");
                return;
            }
            Console.WriteLine($"Books borrowed by {userReturn}:");
            for (int i = 0; i < borrowed.Count; i++)
                Console.WriteLine($"{i + 1}. {borrowed[i]}");
            Console.Write("Enter the number of the book to return: ");
            string? inputIndex = Console.ReadLine();
            if (!int.TryParse(inputIndex, out int bookIndex) || bookIndex < 1 || bookIndex > borrowed.Count)
            {
                Console.WriteLine("Invalid selection.");
                return;
            }
            string bookToReturn = borrowed[bookIndex - 1];
            if (library.ReturnBook(userReturn, bookToReturn))
                Console.WriteLine($"{userReturn} has returned the book '{bookToReturn}'.");
            else
                Console.WriteLine($"{userReturn} does not have that book borrowed.");
        }
    }
}
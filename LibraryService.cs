using System;
using System.Collections.Generic;

namespace CsFinal
{
    public class LibraryService
    {
        private readonly string[] books = new string[5];
        private readonly Dictionary<string, List<string>> borrowedBooks = new(); // user -> list of books
        private readonly HashSet<string> checkedOutBooks = new(); // checked out books

        public bool AddBook(string? title)
        {
            if (string.IsNullOrWhiteSpace(title)) return false;
            if (Array.Exists(books, b => b != null && b.Equals(title, StringComparison.OrdinalIgnoreCase))) return false;
            for (int i = 0; i < books.Length; i++)
            {
                if (string.IsNullOrEmpty(books[i]))
                {
                    books[i] = title;
                    return true;
                }
            }
            return false;
        }

        public bool RemoveBook(string? title)
        {
            if (string.IsNullOrWhiteSpace(title)) return false;
            for (int i = 0; i < books.Length; i++)
            {
                if (!string.IsNullOrEmpty(books[i]) && books[i].Equals(title, StringComparison.OrdinalIgnoreCase))
                {
                    books[i] = string.Empty;
                    return true;
                }
            }
            return false;
        }

        public string[] GetBooks()
        {
            return Array.FindAll(books, b => !string.IsNullOrEmpty(b));
        }

        public bool IsFull() => Array.TrueForAll(books, b => !string.IsNullOrEmpty(b));
        public bool IsEmpty() => Array.TrueForAll(books, b => string.IsNullOrEmpty(b));

        public bool SearchBook(string? title)
        {
            if (string.IsNullOrWhiteSpace(title)) return false;
            return Array.Exists(books, b => !string.IsNullOrEmpty(b) && b.Equals(title, StringComparison.OrdinalIgnoreCase));
        }

        public bool BorrowBook(string? user, string? title)
        {
            if (string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(title)) return false;
            if (!SearchBook(title)) return false;
            if (checkedOutBooks.Contains(title)) return false;
            if (!borrowedBooks.ContainsKey(user)) borrowedBooks[user] = new List<string>();
            if (borrowedBooks[user].Count >= 3) return false;
            borrowedBooks[user].Add(title);
            checkedOutBooks.Add(title);
            return true;
        }

        public bool ReturnBook(string? user, string? title)
        {
            if (string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(title)) return false;
            if (!borrowedBooks.ContainsKey(user)) return false;
            if (!borrowedBooks[user].Contains(title)) return false;
            borrowedBooks[user].Remove(title);
            checkedOutBooks.Remove(title);
            return true;
        }

        public bool IsBookCheckedOut(string? title)
        {
            if (string.IsNullOrWhiteSpace(title)) return false;
            return checkedOutBooks.Contains(title);
        }

        public int BorrowedCount(string user)
        {
            if (!borrowedBooks.ContainsKey(user)) return 0;
            return borrowedBooks[user].Count;
        }

        public List<string> GetBorrowedBooks(string user)
        {
            if (!borrowedBooks.ContainsKey(user)) return new List<string>();
            return new List<string>(borrowedBooks[user]);
        }
    }
}

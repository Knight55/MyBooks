using System.Collections.Generic;
using MyBooks.Dal.Entities;

namespace MyBooks.Bll.Services
{
    public interface IBookService
    {
        Book GetBook(int bookId);
        IEnumerable<Book> GetBooks();
        IEnumerable<Book> SearchBooks(string searchTerm);
        Book InsertBook(Book newBook);
        void UpdateBook(int bookId, Book updatedBook);
        void DeleteBook(int bookId);
    }
}
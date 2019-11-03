using System.Collections.Generic;
using MyBooks.Dal.Entities;

namespace MyBooks.Bll.Services
{
    public interface IBookService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
        Book GetBook(int bookId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="goodreadsId"></param>
        /// <returns></returns>
        Book GetBook(string goodreadsId);
        IEnumerable<Book> GetBooks();
        IEnumerable<Book> SearchBooks(string searchTerm);
        Book InsertBook(Book newBook);
        void UpdateBook(int bookId, Book updatedBook);
        void DeleteBook(int bookId);
    }
}
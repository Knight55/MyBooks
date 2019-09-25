using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MyBooks.Bll.Context;
using MyBooks.Bll.Entities;
using MyBooks.Bll.Exceptions;

namespace MyBooks.Bll.Services
{
    public class BookService : IBookService
    {
        private readonly ApplicationDbContext _context;

        public BookService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Book GetBook(int bookId)
        {
            return _context.Books
                .Include(b => b.Editions)
                .ThenInclude(e => e.Publisher)
                .Include(b => b.BookAuthors)
                .ThenInclude(w => w.Author)
                .SingleOrDefault(b => b.Id == bookId) ?? throw new EntityNotFoundException("Book not found.");
        }

        public IEnumerable<Book> GetBooks()
        {
            var books = _context.Books
                .Include(b => b.Editions)
                .ThenInclude(e => e.Publisher)
                .Include(b => b.BookAuthors)
                .ThenInclude(w => w.Author)
                .ToList();

            return books;
        }

        public IEnumerable<Book> SearchBooks(string searchTerm)
        {
            var books = _context.Books
                .Where(b => b.Title.Contains(searchTerm))
                .Include(b => b.Editions)
                .ThenInclude(e => e.Publisher)
                .Include(b => b.BookAuthors)
                .ThenInclude(w => w.Author)
                .ToList();

            return books;
        }

        public Book InsertBook(Book newBook)
        {
            _context.Books.Add(newBook);
            _context.SaveChanges();
            return newBook;
        }

        public void UpdateBook(int bookId, Book updatedBook)
        {
            updatedBook.Id = bookId;
            var entry = _context.Attach(updatedBook);
            entry.State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                var book = _context.Books.AsNoTracking().SingleOrDefault(b => b.Id == bookId)
                           ?? throw new EntityNotFoundException("Book not found.");

                throw;
            }
        }

        public void DeleteBook(int bookId)
        {
            _context.Books.Remove(new Book { Id = bookId });

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                var book = _context.Books.AsNoTracking().SingleOrDefault(b => b.Id == bookId)
                           ?? throw new EntityNotFoundException("Book not found");

                throw;
            }
        }
    }
}
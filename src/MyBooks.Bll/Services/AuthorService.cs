using System.Collections.Generic;
using MyBooks.Dal.Context;
using MyBooks.Dal.Entities;

namespace MyBooks.Bll.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly ApplicationDbContext _context;

        public AuthorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Author GetAuthor(int authorId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Author> GetAuthors()
        {
            throw new System.NotImplementedException();
        }

        public Author InsertAuthor(Author newAuthor)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateAuthor(int authorId, Author updatedAuthor)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteAuthor(int authorId)
        {
            throw new System.NotImplementedException();
        }
    }
}
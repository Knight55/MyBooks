using System.Collections.Generic;
using MyBooks.Bll.Entities;

namespace MyBooks.Bll.Services
{
    public interface IAuthorService
    {
        Author GetAuthor(int authorId);
        IEnumerable<Author> GetAuthors();
        Author InsertAuthor(Author newAuthor);
        void UpdateAuthor(int authorId, Author updatedAuthor);
        void DeleteAuthor(int authorId);
    }
}
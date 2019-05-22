using System.Collections.Generic;
using System.Threading.Tasks;
using MyBooks.Dto.Dtos;
using Refit;

namespace MyBooks.Client.Services
{
    public interface IMyBookApiService
    {
        [Get("/api/Books/{id}")]
        Task<Book> GetBookAsync(int id);

        [Get("/api/Books")]
        Task<List<Book>> GetBooksAsync();

        [Get("/api/Books/Search/{searchTerm}")]
        Task<List<Book>> SearchBooksAsync(string searchTerm);

        [Post("/api/Books")]
        Task<Book> InsertBookAsync([Body] Book book);

        [Put("/api/Books/{id}")]
        Task UpdateBookAsync(int id, [Body] Book book);

        [Delete("/api/Books/{id}")]
        Task DeleteBookAsync(int id);
    }
}
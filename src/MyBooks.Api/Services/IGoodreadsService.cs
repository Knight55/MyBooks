using System.Threading.Tasks;
using MyBooks.Dto.Goodreads;

namespace MyBooks.Api.Services
{
    /// <summary>
    /// Public interface for accessing the Goodreads API.
    /// </summary>
    public interface IGoodreadsService
    {
        /// <summary>
        /// Searches for books on Goodreads API.
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <returns>
        /// A <see cref="GoodreadsResponse"/> containing the books.
        /// </returns>
        Task<GoodreadsResponse> SearchBooks(string searchTerm);

        /// <summary>
        /// Gets a specific book from Goodreads.
        /// </summary>
        /// <param name="id">The identifier of the book.</param>
        /// <returns>
        /// A <see cref="GoodreadsResponse"/> containing the book.
        /// </returns>
        Task<GoodreadsResponse> GetBook(string id);

        /// <summary>
        /// Gets a specific author from Goodreads.
        /// </summary>
        /// <param name="id">The identifier of the author</param>
        /// <returns>
        /// A <see cref="GoodreadsResponse"/> containing the author.
        /// </returns>
        Task<GoodreadsResponse> GetAuthor(string id);
    }
}
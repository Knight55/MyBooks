using System.Collections.Generic;
using MyBooks.Dal.Entities;

namespace MyBooks.Bll.Services
{
    public interface IRatingService
    {
        Rating GetRating(int id);
        IEnumerable<Rating> GetRatingsForBook(int bookId);
        Rating InsertRating(Rating newRating);
        void UpdateRating(int ratingId, Rating updatedRating);
        void DeleteRating(int ratingId);
    }
}
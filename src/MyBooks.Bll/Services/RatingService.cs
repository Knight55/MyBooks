using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MyBooks.Bll.Exceptions;
using MyBooks.Dal.Context;
using MyBooks.Dal.Entities;

namespace MyBooks.Bll.Services
{
    public class RatingService : IRatingService
    {
        private readonly ApplicationDbContext _context;

        public RatingService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Rating GetRating(int id)
        {
            return _context.Ratings
                       .Include(r => r.Book)
                       .SingleOrDefault(b => b.Id == id) ?? throw new EntityNotFoundException("Rating not found.");
        }

        public IEnumerable<Rating> GetRatingsForBook(int bookId)
        {
            return _context.Ratings
                .Where(r => r.BookId == bookId)
                .ToList();

        }

        public Rating InsertRating(Rating newRating)
        {
            _context.Ratings.Add(newRating);
            _context.SaveChanges();
            return newRating;
        }

        public void UpdateRating(int ratingId, Rating updatedRating)
        {
            updatedRating.Id = ratingId;
            var entry = _context.Attach(updatedRating);
            entry.State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                var rating = _context.Ratings.AsNoTracking().SingleOrDefault(r => r.Id == ratingId)
                           ?? throw new EntityNotFoundException("Book not found.");

                throw;
            }
        }

        public void DeleteRating(int ratingId)
        {
            _context.Ratings.Remove(new Rating {Id = ratingId});

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                var rating = _context.Ratings.AsNoTracking().SingleOrDefault(r => r.Id == ratingId)
                             ?? throw new EntityNotFoundException("Book not found.");

                throw;
            }
        }
    }
}
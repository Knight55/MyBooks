using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyBooks.Dal.Entities;

namespace MyBooks.Dal.Context
{
    public class ApplicationDbContext : IdentityUserContext<ApplicationUser, long>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.UseHiLo();

            builder.Entity<Edition>()
                .Property(e => e.Format)
                .HasConversion<string>()
                .HasDefaultValue(Format.Paperback);

            builder.Entity<Edition>()
                .HasAlternateKey(e => e.IsbnNumber);
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Edition> Editions { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<ReadingStatus> ReadingStatuses { get; set; }
    }
}
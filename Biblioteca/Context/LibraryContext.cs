using Biblioteca.Models;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Context
{
    public class LibraryContext: DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Loan> Loans { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<User>()
                .HasMany(e => e.Books)
                .WithMany(e => e.Users)
                .UsingEntity<Loan>();
        }
    }
}

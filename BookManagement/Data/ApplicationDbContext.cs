using BookManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace BookManagement.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Book> Books { get; set; } = default!;
        public DbSet<Category> Categories { get; set; } = default!;
        public DbSet<Author> Authors { get; set; } = default!;
        public DbSet<BookAuthor> BookAuthors { get; set; } = default!;
        public DbSet<BookCategory> BookCategories { get; set; } = default!;
    }
}

using BookManagement.Data;
using BookManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace BookManagement.Seed
{
    public static class AuthorData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ApplicationDbContext>>());

            if (context.Authors.Any())
            {
                return;
            }
            
            context.Authors.AddRange(
                new Author
                {
                    Name = "Author 1"
                },
                new Author
                {
                    Name = "Author 2"
                },
                new Author
                {
                    Name = "Author 3"
                },
                new Author
                {
                    Name = "Author 4"
                }
            );
            context.SaveChanges();
        }
    }
}

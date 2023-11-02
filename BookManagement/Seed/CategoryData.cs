using BookManagement.Data;
using BookManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace BookManagement.Seed
{
    public static class CategoryData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ApplicationDbContext>>());

            if (context.Categories.Any())
            {
                return;
            }

            context.Categories.AddRange(
                new Category
                {
                    Name = "Category 1"
                },
                new Category
                {
                    Name = "Category 2"
                },
                new Category
                {
                    Name = "Category 3"
                },
                new Category
                {
                    Name = "Category 4"
                }
            );
            context.SaveChanges();
        }
    }
}

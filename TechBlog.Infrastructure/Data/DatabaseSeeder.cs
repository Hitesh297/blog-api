using System;
using System.Threading.Tasks;
using TechBlog.Domain.Entities;

namespace TechBlog.Infrastructure.Data;

public class DatabaseSeeder
{
    public static async Task Seed(TechBlogDbContext context)
    {
        if (!context.Blogs.Any())
        {
            context.Blogs.AddRange(
                new Blog
                {
                    Id = Guid.NewGuid(),
                    Title = "Introduction to .NET Core",
                    Content = "This blog explores the basics of .NET Core, a cross-platform framework for building modern applications.",
                    Author = "John Doe",
                    CreatedAt = DateTime.UtcNow.AddDays(-10),
                    ImageUrl = "https://learn.microsoft.com/training/achievements/get-started-c-sharp-part-1-social.png"
                },
            new Blog
            {
                Id = Guid.NewGuid(),
                Title = "Getting Started with React",
                Content = "Learn how to set up a React application and build dynamic user interfaces.",
                Author = "Jane Smith",
                CreatedAt = DateTime.UtcNow.AddDays(-5),
                ImageUrl = "https://learn.microsoft.com/training/achievements/get-started-c-sharp-part-1-social.png"
            },
            new Blog
            {
                Id = Guid.NewGuid(),
                Title = "Combining .NET Core and React",
                Content = "A guide to building a full-stack application using .NET Core for the backend and React for the frontend.",
                Author = "Alice Johnson",
                CreatedAt = DateTime.UtcNow.AddDays(-2),
                ImageUrl = "https://learn.microsoft.com/training/achievements/get-started-c-sharp-part-1-social.png"
            },
            new Blog
                {
                    Id = Guid.NewGuid(),
                    Title = "Introduction to .NET Core",
                    Content = "This blog explores the basics of .NET Core, a cross-platform framework for building modern applications.",
                    Author = "John Doe",
                    CreatedAt = DateTime.UtcNow.AddDays(-10),
                    ImageUrl = "https://learn.microsoft.com/training/achievements/get-started-c-sharp-part-1-social.png"
                },
            new Blog
            {
                Id = Guid.NewGuid(),
                Title = "Getting Started with React",
                Content = "Learn how to set up a React application and build dynamic user interfaces.",
                Author = "Jane Smith",
                CreatedAt = DateTime.UtcNow.AddDays(-5),
                ImageUrl = "https://learn.microsoft.com/training/achievements/get-started-c-sharp-part-1-social.png"
            },
            new Blog
            {
                Id = Guid.NewGuid(),
                Title = "Combining .NET Core and React",
                Content = "A guide to building a full-stack application using .NET Core for the backend and React for the frontend.",
                Author = "Alice Johnson",
                CreatedAt = DateTime.UtcNow.AddDays(-2),
                ImageUrl = "https://learn.microsoft.com/training/achievements/get-started-c-sharp-part-1-social.png"
            }
            );
            await context.SaveChangesAsync();
        }
    }

}

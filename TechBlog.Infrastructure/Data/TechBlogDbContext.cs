using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TechBlog.Domain.Entities;

namespace TechBlog.Infrastructure.Data;

public class TechBlogDbContext : IdentityDbContext<IdentityUser>
{
    public TechBlogDbContext(DbContextOptions<TechBlogDbContext> options)
            : base(options)
        {
        }

        public DbSet<Blog> Blogs { get; set; }

}

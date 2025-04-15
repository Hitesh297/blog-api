using System;
using Microsoft.EntityFrameworkCore;
using TechBlog.Domain.Entities;
using TechBlog.Domain.Interfaces;
using TechBlog.Infrastructure.Data;

namespace TechBlog.Infrastructure.Repositories;

public class BlogRepository : IBlogRepository
{
    private readonly TechBlogDbContext _context;
    public BlogRepository(TechBlogDbContext context)
    {
        _context = context;
    }
    public async Task AddAsync(Blog blog, CancellationToken cancellationToken)
    {
        blog.Id = Guid.NewGuid();
        blog.CreatedAt = DateTime.UtcNow;
        await _context.Blogs.AddAsync(blog, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var blog = await GetByIdAsync(id, cancellationToken);
        _context.Blogs.Remove(blog);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<Blog>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Blogs.ToListAsync(cancellationToken);
    }

    public async Task<Blog> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Blogs.FindAsync(new object[] { id }, cancellationToken)
                ?? throw new KeyNotFoundException("Blog not found");
    }

    public async Task UpdateAsync(Blog blog, CancellationToken cancellationToken)
    {
        var existingBlog = await GetByIdAsync(blog.Id, cancellationToken);
        existingBlog.Title = blog.Title;
        existingBlog.Content = blog.Content;
        existingBlog.Author = blog.Author;
        existingBlog.UpdatedAt = DateTime.UtcNow;
        _context.Blogs.Update(existingBlog);
        await _context.SaveChangesAsync(cancellationToken);
    }
}

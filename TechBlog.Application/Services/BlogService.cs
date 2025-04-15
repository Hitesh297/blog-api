using System;
using TechBlog.Application.DTOs;
using TechBlog.Application.Interfaces;
using TechBlog.Domain.Entities;
using TechBlog.Domain.Interfaces;

namespace TechBlog.Application.Services;

public class BlogService : IBlogService
{
    private readonly IBlogRepository _blogRepository;
    public BlogService(IBlogRepository blogRepository)
    {
        _blogRepository = blogRepository;
    }
    public async Task<BlogDto> CreateAsync(CreateBlogDto createDto, CancellationToken cancellationToken)
    {
        var blog = new Blog
            {
                Title = createDto.Title,
                Content = createDto.Content,
                Author = createDto.Author,
                ImageUrl = createDto.ImageUrl
                
            };
            await _blogRepository.AddAsync(blog, cancellationToken);
            return MapToDto(blog);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        await _blogRepository.DeleteAsync(id, cancellationToken);
    }

    public async Task<IEnumerable<BlogDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var blogs = await _blogRepository.GetAllAsync(cancellationToken);
            return blogs.Select(MapToDto);
    }

    public async Task<BlogDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var blog = await _blogRepository.GetByIdAsync(id, cancellationToken);
            return MapToDto(blog);
    }

    public async Task UpdateAsync(Guid id, UpdateBlogDto updateDto, CancellationToken cancellationToken)
    {
        var blog = new Blog
            {
                Id = id,
                Title = updateDto.Title,
                Content = updateDto.Content,
                Author = updateDto.Author,
                ImageUrl = updateDto.ImageUrl
            };
            await _blogRepository.UpdateAsync(blog, cancellationToken);
    }

    private static BlogDto MapToDto(Blog blog)
        {
            return new BlogDto
            {
                Id = blog.Id,
                Title = blog.Title,
                Content = blog.Content,
                Author = blog.Author,
                CreatedAt = blog.CreatedAt,
                UpdatedAt = blog.UpdatedAt,
                ImageUrl = blog.ImageUrl
            };
        }
}

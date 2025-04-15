using System;
using TechBlog.Application.DTOs;

namespace TechBlog.Application.Interfaces;

public interface IBlogService
{
    Task<BlogDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<BlogDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<BlogDto> CreateAsync(CreateBlogDto createDto, CancellationToken cancellationToken);
    Task UpdateAsync(Guid id, UpdateBlogDto updateDto, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}

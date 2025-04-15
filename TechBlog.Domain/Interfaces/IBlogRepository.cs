using System;
using TechBlog.Domain.Entities;

namespace TechBlog.Domain.Interfaces;

public interface IBlogRepository
{
    Task<Blog> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<Blog>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Blog blog, CancellationToken cancellationToken);
    Task UpdateAsync(Blog blog, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);

}

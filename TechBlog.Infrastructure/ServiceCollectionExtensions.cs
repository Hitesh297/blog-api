using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TechBlog.Domain.Interfaces;
using TechBlog.Infrastructure.Data;
using TechBlog.Infrastructure.Repositories;

namespace TechBlog.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<TechBlogDbContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString),
                mysqlOptions => mysqlOptions.EnableRetryOnFailure()));

        services.AddScoped<IBlogRepository, BlogRepository>();
        return services;
    }

}

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using TechBlog.Domain.Interfaces;
using TechBlog.Infrastructure.Data;
using TechBlog.Infrastructure.Repositories;

namespace TechBlog.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<TechBlogDbContext>(opt =>
        opt.UseSqlServer(config.GetConnectionString("DefaultConnection")));

        services.AddScoped<IBlogRepository, BlogRepository>();
        return services;
    }

}

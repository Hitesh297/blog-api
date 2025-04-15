using System;
using Microsoft.Extensions.DependencyInjection;
using TechBlog.Application.Interfaces;
using TechBlog.Application.Services;

namespace TechBlog.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IBlogService, BlogService>();
            return services;
        }

}

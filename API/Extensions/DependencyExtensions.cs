using API.Filters;
using Core.Entities;
using Core.Interfaces;
using Core.ServicesInterfaces;
using Infrastructure.Repository;
using Infrastructure.Services;

namespace API.Extensions
{
    public static class DependencyExtensions
    {
        public static IServiceCollection
        DependencyExtend(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            services
                .Configure<CloudinaryOptions>(configuration
                    .GetSection("CloudinaryOptions"));


            services.AddScoped<IPhotoService, PhotoService>();
            services.AddScoped<LoginValidationAttribute>();


            services.AddScoped<IBlogRepository, BlogRepository>();
            services.AddScoped<IBlogCommentRepository, BlogCommentRepository>();
            services.AddScoped<IPhotoRepository, PhotoRepository>();

            return services;
        }
    }
}

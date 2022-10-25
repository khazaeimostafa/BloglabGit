using Core.Models;
using Core.ServicesInterfaces;
using Infrastructure.Services;
using Met.Data;
using Met.Entities;
using Met.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class IdentityExtension
    {
        public static IServiceCollection
        AddMet(this IServiceCollection Services, IConfiguration configuration)
        {
            Services
                .AddIdentity<AppUser, IdentityRole>(x =>
                {
                    x.Password.RequireNonAlphanumeric = false;
                    x.Password.RequiredLength = 5;
                    x.Password.RequireDigit = false;
                    x.Password.RequireUppercase = false;
                    x.Password.RequireNonAlphanumeric = false;
                    x.Password.RequireLowercase = false;
                })
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddSignInManager<SignInManager<AppUser>>()
                .AddDefaultTokenProviders();

            Services.AddScoped<TokenUrlEncoderService>();
            Services.AddScoped<IEmailSender, EmailSender>();
            Services.AddScoped<IdentityEmailService>();

            return Services;
        }
    }
}

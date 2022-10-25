using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions
{
    public static class JwtExtension
    {
        public static IServiceCollection
        JwtExtend(
            this IServiceCollection services,
            IConfiguration configuration,
            TokenValidationParameters tokenValidationParameters
        )
        {
            services
                .AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme =
                        JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme =
                        JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                     

                    options.RequireHttpsMetadata = false;
 
                    options.TokenValidationParameters =
                        tokenValidationParameters;
                });
            return services;
        }
    }
}

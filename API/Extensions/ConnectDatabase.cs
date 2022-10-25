using System.Collections.Immutable;
using Met.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class ConnectDatabase
    {
        public static IServiceCollection
        ConnectDatabaseExtend(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            // services
            //     .AddDbContext<AppIdentityDbContext>(x =>
            //         x
            //             .UseSqlite(configuration
            //                 .GetConnectionString("IdentityConnection")));
            services
                .AddDbContext<AppIdentityDbContext>(options =>
                    options
                        .UseSqlServer(configuration
                            .GetConnectionString("IdentityConnection")));

            return services;
        }
    }
}

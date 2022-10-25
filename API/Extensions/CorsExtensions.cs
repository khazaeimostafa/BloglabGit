namespace API.Extensions
{
    public static class CorsExtensions
    {
        public static IServiceCollection
        CorsExtend(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            services
                .AddCors(opt =>
                {
                    opt
                        .AddPolicy("CorsPolicy",
                        policy =>
                        {
                            policy
                                .AllowAnyHeader()
                                .AllowAnyMethod()
                                .AllowAnyOrigin();
                        });
                });

            return services;
        }
    }
}

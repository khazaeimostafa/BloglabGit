using API.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class ErrorExtension
    {
        public static IServiceCollection ApiErrorExtension(this IServiceCollection services)
        {

            services.Configure<ApiBehaviorOptions>(x =>
            {
                x.InvalidModelStateResponseFactory =
                actionContext =>
                {
                    string[] errors = actionContext.ModelState.Where(e => e.Value.Errors.Count > 0)
                        .SelectMany(x => x.Value.Errors)
                        .Select(x => x.ErrorMessage).ToArray();

                    ApiValidationResponse errorResponse = new ApiValidationResponse
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(errorResponse);

                };
            });
            return services;
        }
    }
}

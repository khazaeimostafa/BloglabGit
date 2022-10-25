using System.Net;
using Core.Entities;
using Microsoft.AspNetCore.Diagnostics;

namespace API.MiddleWares
{
    public static class ExceptionMiddleWareExtensions
    {
        public static void ConfigureExceptionHandler(
            this IApplicationBuilder app
        )
        {
            app.UseExceptionHandler(appError =>
                {
                    appError
                        .Run(async context =>
                        {
                            context.Response.StatusCode =
                                (int)HttpStatusCode.InternalServerError;

                            context.Response.ContentType = "application/json";

                            IExceptionHandlerFeature contextFeature =
                                context
                                    .Features
                                    .Get<IExceptionHandlerFeature>();

                            if (contextFeature != null)
                            {
                                await context
                                    .Response
                                    .WriteAsync(new ApiException()
                                    {
                                        StatusCode =
                                            context.Response.StatusCode,
                                        Message = "Internal Server Error"
                                    }.ToString());
                            }
                        });
                });
        }
    }
}

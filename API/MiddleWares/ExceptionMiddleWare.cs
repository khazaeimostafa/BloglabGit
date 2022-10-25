using System.Net;

namespace API.MiddleWares
{
    public class ExceptionMiddleWare
    {


        public readonly RequestDelegate _next;

        public readonly ILogger<ExceptionMiddleWare> _logger;
        public readonly IHostEnvironment _env;
        public ExceptionMiddleWare(RequestDelegate next, ILogger<ExceptionMiddleWare> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async
             Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                //var response  =_env.IsDevelopment()?  new ApiException()

            }
        }

    }
}


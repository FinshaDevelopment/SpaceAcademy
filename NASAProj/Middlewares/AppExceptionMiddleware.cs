using NASAProj.Service.Exceptions;
using System.Net;

namespace NASAProj.Middlewares
{
    public class AppExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<AppExceptionMiddleware> logger;
        public AppExceptionMiddleware(RequestDelegate next, ILogger<AppExceptionMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await this.next.Invoke(context);
            }
            catch (HttpStatusCodeException ex)
            {
                await this.HandleException(context, ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                //Log
                logger.LogError(ex.ToString());

                await this.HandleException(context, 500, ex.Message);
            }
        }

        public async Task HandleException(HttpContext context, int code, string message)
        {
            context.Response.StatusCode = code;
            await context.Response.WriteAsJsonAsync(new
            {
                Code = code,
                Message = message
            });
        }
    }
}

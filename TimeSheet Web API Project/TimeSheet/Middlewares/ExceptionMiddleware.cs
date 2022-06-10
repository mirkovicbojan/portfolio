using System.Net;
using TimeSheet.Contracts;
using TimeSheet.CustomExceptions;
using TimeSheet.Models;

namespace TimeSheet.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerManager _logger;
        public ExceptionMiddleware(RequestDelegate next, ILoggerManager logger)
        {
            _logger = logger;
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (BaseCustomException ex)
            {
                await HandleExceptionAsync(httpContext, (int)ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, (int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, int statusCode, string message)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = statusCode,
                Message = message
            }.ToString());
        }
    }
}
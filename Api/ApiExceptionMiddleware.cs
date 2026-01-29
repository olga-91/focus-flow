using System.Net;
using System.Net.Mime;
using Application.Exceptions;

namespace Api;

public class ApiExceptionMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = MediaTypeNames.Application.Json;
        context.Response.StatusCode = exception is BusinessException businessException
            ? businessException.StatusCode
            : (int)HttpStatusCode.InternalServerError;

        await context.Response.WriteAsync(exception.Message);
    }
}
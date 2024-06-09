using Notes.Application.Common.CustomExceptions;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;

namespace Notes.WebApi.Middleware;

public class CustomExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public CustomExceptionHandlerMiddleware(RequestDelegate next, ILogger logger) => 
        (_next, _logger) = (next, logger);
    
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private  Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        var statusCode = HttpStatusCode.InternalServerError;
        var result = string.Empty;
        switch (ex)
        {
            case FluentValidation.ValidationException exception:
                result = JsonSerializer.Serialize(exception);
                statusCode = HttpStatusCode.BadRequest;
                break;
            case NotFoundException exception:
                statusCode = HttpStatusCode.NotFound;
                break;
            case TaskCanceledException taskException:
                result = JsonSerializer.Serialize(taskException);
                statusCode = HttpStatusCode.BadRequest;
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;
        if (result == string.Empty)
        {
            result = JsonSerializer.Serialize(new {error = ex.Message});
        }

        return context.Response.WriteAsync(result);
    }
}

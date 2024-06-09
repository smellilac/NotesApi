namespace Notes.WebApi.Middleware;

public static class CustomExceptionHandlerMiddlewareExtensions
{
    public static IApplicationBuilder UseCastomExceptionHandler(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
    }
}

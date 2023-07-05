using PdfGenerator.WebApi.Middlewares;

namespace PdfGenerator.WebApi.Extensions;

public static class AppExtensions
{
    public static void UseErrorHandlerMiddleware(this IApplicationBuilder theAppBuilder)
    {
        theAppBuilder.UseMiddleware<ErrorHandlerMiddleware>();
    }
}
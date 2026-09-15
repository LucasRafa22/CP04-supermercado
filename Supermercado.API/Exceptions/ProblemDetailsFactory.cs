using Microsoft.AspNetCore.Mvc;

namespace Supermercado.API.Exceptions;

public static class ProblemDetailsFactory
{
    public static ProblemDetails Create(Exception ex, HttpContext context, int statusCode)
    {
        return new ProblemDetails
        {
            Title = "Erro na aplicação",
            Detail = ex.Message,
            Status = statusCode,
            Instance = context.Request.Path,
            Type = GetTypeUri(statusCode)
        };
    }

    private static string GetTypeUri(int statusCode)
    {
        return statusCode switch
        {
            400 => "https://httpstatuses.com/400",
            404 => "https://httpstatuses.com/404",
            500 => "https://httpstatuses.com/500",
            _ => "https://httpstatuses.com/" + statusCode
        };
    }
}
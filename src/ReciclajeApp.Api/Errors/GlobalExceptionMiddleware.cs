using System.Text.Json;

namespace ReciclajeApp.Api.Errors;

public sealed class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var (statusCode, type, message) = exception switch
        {
            ArgumentException => (StatusCodes.Status400BadRequest, "validation_error", "La solicitud contiene errores de validacion."),
            KeyNotFoundException => (StatusCodes.Status404NotFound, "not_found", "No se encontro el recurso solicitado."),
            InvalidOperationException => (StatusCodes.Status409Conflict, "conflict", "La operacion entra en conflicto con el estado actual."),
            _ => (StatusCodes.Status500InternalServerError, "internal_error", "Ha ocurrido un error interno.")
        };

        var response = new ApiErrorResponse
        {
            Type = type,
            Message = message,
            Errors = new[] { exception.Message },
            TraceId = context.TraceIdentifier
        };

        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";

        var json = JsonSerializer.Serialize(response);
        await context.Response.WriteAsync(json);
    }
}

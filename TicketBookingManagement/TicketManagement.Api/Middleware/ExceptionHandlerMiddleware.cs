using System.Net;
using System.Text.Json;
using TicketManagement.Application.Exceptions;

namespace TicketManagement.Api.Middleware;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _nextHandler;

    public ExceptionHandlerMiddleware(RequestDelegate nextHandler)
    {
        _nextHandler = nextHandler;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _nextHandler(context);
        }
        catch (Exception e)
        {
            await ConvertException(context, e);            
        }
    }

    private async Task<Task> ConvertException(HttpContext context, Exception exception)
    {
        HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;
        context.Response.ContentType = "application/json";
        
        var result= string.Empty;

        switch (exception)
        {
            case ValidationException validationException:
                httpStatusCode = HttpStatusCode.BadRequest;
                result = JsonSerializer.Serialize(validationException.ValdationErrors);
                break;
            case BadRequestException badRequestException:
                httpStatusCode = HttpStatusCode.BadRequest;
                result = badRequestException.Message;
                break;
            case NotFoundException:
                httpStatusCode = HttpStatusCode.NotFound;
                break;
            case Exception:
                httpStatusCode = HttpStatusCode.BadRequest;
                break;
        }
        
        context.Response.StatusCode = (int)httpStatusCode;

        if (result == string.Empty)
        {
            result = JsonSerializer.Serialize(new { error = exception.Message });
        }

        return context.Response.WriteAsync(result);
    }
}
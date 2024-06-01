using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;
using Common.Entities.Errors;

namespace HabitTrackerAppBackend.Infrastructure.Errors;

public class ExceptionHandlerMiddleware : IMiddleware
{
    
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (JsonException ex) when (ex.Message.Contains("could not be converted to System.Guid"))
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsync("404 Not Found");
        }
        catch (InvalidOperationException e)
        {
            await HandleExceptionAsync(context, e);
        }
        catch (System.IO.DirectoryNotFoundException exception)
        {
            context.Response.StatusCode = 404    ;
            await context.Response.WriteAsync(exception.Message);
        }
        catch (Exceptions e)
        {
            context.Response.Clear();
            context.Response.StatusCode = (int)e.StatusCode;
            await context.Response.WriteAsync(e.Error.ToString());
        }
        catch (Exception e)
        {
            await HandleExceptionAsync(context, e);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError;
        
        if (exception is ValidationException)
            code = HttpStatusCode.NotFound;

        if (exception is InvalidOperationException)
            code = HttpStatusCode.NotFound;

        var result = JsonSerializer.Serialize(new { Error = exception.Message });
        
        context.Response.StatusCode = (int)code;

        return context.Response.WriteAsync(result);

    }
}
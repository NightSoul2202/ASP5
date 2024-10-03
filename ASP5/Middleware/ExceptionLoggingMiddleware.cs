using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

public class ExceptionLoggingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            LogExceptionToFile(ex);

            if (!context.Response.HasStarted)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync($"Error on server: {DateTime.Now}: {ex.Message}");
            }
        }
    }

    private void LogExceptionToFile(Exception ex)
    {
        var logFilePath = "logs/exceptions.log";

        if (!Directory.Exists("logs"))
        {
            Directory.CreateDirectory("logs");
        }

        var logMessage = $"{DateTime.Now}: {ex.Message} {Environment.NewLine} {ex.StackTrace}{Environment.NewLine}";

        File.AppendAllText(logFilePath, logMessage);
    }
}

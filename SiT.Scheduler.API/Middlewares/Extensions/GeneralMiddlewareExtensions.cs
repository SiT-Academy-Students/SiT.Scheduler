namespace SiT.Scheduler.API.Middlewares.Extensions;

using System;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;

public static class GeneralMiddlewareExtensions
{
    public static async Task BadRequest([NotNull] this HttpContext httpContext, [NotNull] string message)
    {
        if (httpContext is null) throw new ArgumentNullException(nameof(httpContext));
        if (string.IsNullOrWhiteSpace(message)) throw new ArgumentNullException(nameof(message));

        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        var messageBytes = Encoding.UTF8.GetBytes(message);
        await httpContext.Response.Body.WriteAsync(messageBytes, 0, messageBytes.Length);
        await httpContext.Response.CompleteAsync();
    }
}
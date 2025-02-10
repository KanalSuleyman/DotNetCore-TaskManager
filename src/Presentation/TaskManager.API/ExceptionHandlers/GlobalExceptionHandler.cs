using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using TaskManager.Application;

namespace TaskManager.API.ExceptionHandlers;

/// <summary>
/// Handles all unhandled exceptions globally and returns a standardized error response.
/// This handler ensures that all exceptions are caught and returned in a consistent format.
/// </summary>
public class GlobalExceptionHandler : IExceptionHandler
{
    /// <summary>
    /// Attempts to handle the exception asynchronously.
    /// This method creates a standardized error response and writes it to the HTTP response.
    /// </summary>
    /// <param name="httpContext">The HTTP context associated with the request.</param>
    /// <param name="exception">The exception to handle.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation. The task result indicates whether the exception was handled.</returns>
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        // Creates a standardized error response using the ServiceResult class.
        var errorDto = ServiceResult.Failure(exception.Message, HttpStatusCode.InternalServerError);

        // Sets the HTTP response status code and content type.
        httpContext.Response.StatusCode = (int)errorDto.StatusCode;
        httpContext.Response.ContentType = "application/json";

        // Writes the error response as JSON to the HTTP response body.
        await httpContext.Response.WriteAsJsonAsync(errorDto, cancellationToken);

        // Returns true to indicate that the exception was handled.
        return true;
    }
}
using Microsoft.AspNetCore.Diagnostics;
using TaskManager.Domain.Exceptions;

namespace TaskManager.API.ExceptionHandlers;

public class CriticalExceptionHandler : IExceptionHandler
{
    /// <summary>
    /// Attempts to handle the exception asynchronously.
    /// If the exception is a <see cref="CriticalException"/>, it logs the exception and simulates sending an SMS notification.
    /// </summary>
    /// <param name="httpContext">The HTTP context associated with the request.</param>
    /// <param name="exception">The exception to handle.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation. The task result indicates whether the exception was handled.</returns>
    public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is CriticalException)
            Console.WriteLine("Critical Exception is handled. An SMS Sent About the Critical Exception");

        return ValueTask.FromResult(false);
    }
}
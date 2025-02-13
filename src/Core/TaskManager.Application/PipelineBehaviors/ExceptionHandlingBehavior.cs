using System.Net;
using MediatR;
using Microsoft.Extensions.Logging;

namespace TaskManager.Application.PipelineBehaviors;

public class ExceptionHandlingBehavior<TRequest, TResponse>(
    ILogger<ExceptionHandlingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex,
                "Unhandled exception for request {RequestName} {@Request}",
                typeof(TRequest).Name,
                request);

            var statusCode = GetStatusCode(ex);

            if (typeof(TResponse).IsGenericType &&
                typeof(TResponse).GetGenericTypeDefinition() == typeof(ServiceResult<>))
            {
                var innerType = typeof(TResponse).GetGenericArguments()[0];
                var failureResponse = typeof(ServiceResult<>)
                    .MakeGenericType(innerType)
                    .GetMethod("Failure", new[] { typeof(string), typeof(HttpStatusCode) });

                if (failureResponse is null)
                    throw new InvalidOperationException("Failure method not found on ServiceResult.");

                var result = failureResponse.Invoke(null,
                    new object[] { "An error occurred while processing the request.", statusCode });
                return (TResponse)result!;
            }

            throw;
        }
    }

    private static HttpStatusCode GetStatusCode(Exception ex)
    {
        return ex switch
        {
            ArgumentException _ => HttpStatusCode.BadRequest,
            UnauthorizedAccessException _ => HttpStatusCode.Unauthorized,
            _ => HttpStatusCode.InternalServerError
        };
    }
}
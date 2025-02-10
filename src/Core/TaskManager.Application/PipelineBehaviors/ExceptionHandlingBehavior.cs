using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Application.PipelineBehaviors
{
    public class ExceptionHandlingBehavior<TRequest, TResponse>(
        ILogger<ExceptionHandlingBehavior<TRequest, TResponse>> logger)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private HttpStatusCode GetStatusCode(Exception ex)
        {
            // Otherwise, map known exceptions or use a default
            return ex switch
            {
                ArgumentException _ => HttpStatusCode.BadRequest,
                UnauthorizedAccessException _ => HttpStatusCode.Unauthorized,
                _ => HttpStatusCode.InternalServerError
            };
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            try
            {
                // Call the next delegate/middleware in the pipeline
                return await next();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unhandled exception for request {RequestName} {@Request}", typeof(TRequest).Name, request);

                var statusCode = GetStatusCode(ex);

                // Option: If TResponse is ServiceResult<T>, return a failure result.
                if (typeof(TResponse).IsGenericType && typeof(TResponse).GetGenericTypeDefinition() == typeof(ServiceResult<>))
                {
                    // Create a failure result using your helper method. Adjust for generic support if needed.
                    dynamic failureResult = ServiceResult.Failure("An error occurred while processing the request.", statusCode);
                    return failureResult;
                }

                // Otherwise, rethrow the exception
                throw;
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TaskManager.Application;

namespace TaskManager.API.Filters
{
    public class FluentValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Checks if the model state is invalid.
            if (!context.ModelState.IsValid)
            {
                // Extracts the validation errors from the model state.
                var errors = context.ModelState.Values
                    .SelectMany(x => x.Errors)
                    .Select(x => x.ErrorMessage)
                    .ToList();

                // Creates a failure result with the validation errors.
                var resultModel = ServiceResult.Failure(errors);

                // Returns a bad request response with the validation errors.
                context.Result = new BadRequestObjectResult(resultModel);

                return;
            }

            // Continues with the next action filter or the action itself.
            await next();
        }
    }
}

using TaskManager.API.ExceptionHandlers;

namespace TaskManager.API.Extensions
{
    public static class ExceptionHandlerExtension
    {
        public static IServiceCollection AddExceptionHandlerExtension(this IServiceCollection services)
        {
            services.AddExceptionHandler<CriticalExceptionHandler>();
            services.AddExceptionHandler<GlobalExceptionHandler>();
            return services;
        }
    }
}

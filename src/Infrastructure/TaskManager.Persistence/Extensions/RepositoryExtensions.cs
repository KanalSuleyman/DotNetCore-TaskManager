using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Application.Features.Authentication.Commands.Login;
using TaskManager.Application.Features.Authentication.Commands.Register;
using TaskManager.Application.Features.TaskItem.Commands.CreateTaskItem;
using TaskManager.Application.Features.TaskItem.Commands.DeleteTaskItem;
using TaskManager.Application.Features.TaskItem.Commands.UpdateTaskItem;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Interfaces;
using TaskManager.Domain.Options;
using TaskManager.Persistence.Context;
using TaskManager.Persistence.Interceptors;
using TaskManager.Persistence.Repositories;

namespace TaskManager.Persistence.Extensions
{
    public static class RepositoryExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<TaskManagerDbContext>(options =>
            {
                var connectionStrings =
                    configuration.GetSection(ConnectionStringOption.Key).Get<ConnectionStringOption>();

                options.UseSqlServer(connectionStrings!.SqlServer,
                    sqlServerOptionsAction =>
                    {
                        sqlServerOptionsAction.MigrationsAssembly(typeof(PersistenceAssembly).Assembly.FullName);
                    });
                options.AddInterceptors(new AuditDbContextInterceptor());
            });

            services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<ITaskItemRepository, TaskItemRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(
                    typeof(CreateTaskItemCommandHandler).Assembly,
                    typeof(DeleteTaskItemCommandHandler).Assembly,
                    typeof(UpdateTaskItemCommandHandler).Assembly,
                    typeof(LoginCommandHandler).Assembly,
                    typeof(RegisterCommandHandler).Assembly
                );
            });

            return services;
        }
    }
}

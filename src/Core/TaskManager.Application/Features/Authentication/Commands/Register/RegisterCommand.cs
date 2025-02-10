using MediatR;
using TaskManager.Domain.Enums;

namespace TaskManager.Application.Features.Authentication.Commands.Register;

public sealed record RegisterCommand(string Email, string Password, List<ApplicationRole> Roles) : IRequest<ServiceResult<Guid>>;
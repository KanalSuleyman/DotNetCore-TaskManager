using MediatR;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.Features.Authentication.Commands.Login;

public sealed record LoginCommand(string Email, string Password) : IRequest<ServiceResult<User>>;
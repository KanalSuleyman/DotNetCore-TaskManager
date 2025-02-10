using MediatR;
using System.Net;
using AutoMapper;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.Features.Authentication.Commands.Register;

public class RegisterCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IPasswordHasher passwordHasher) : IRequestHandler<RegisterCommand, ServiceResult<Guid>>
{
    public async Task<ServiceResult<Guid>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await unitOfWork.UserRepository.GetByEmailAsync(request.Email, cancellationToken);

        if (existingUser != null)
        {
            return ServiceResult<Guid>.Failure(
                "User with this email already exists",
                HttpStatusCode.Conflict);
        }

        var user = mapper.Map<User>(request);
        user.PasswordHash = passwordHasher.HashPassword(request.Password);

        await unitOfWork.UserRepository.AddAsync(user, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);

        return ServiceResult<Guid>.SuccessAsCreated(
            user.Id,
            $"/api/users/{user.Id}");
    }
}
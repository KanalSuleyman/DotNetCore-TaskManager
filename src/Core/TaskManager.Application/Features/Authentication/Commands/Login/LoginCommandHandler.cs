using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.Features.Authentication.Commands.Login
{
    public class LoginCommandHandler(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher) : IRequestHandler<LoginCommand, ServiceResult<User>>
    {
        public async Task<ServiceResult<User>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await unitOfWork.UserRepository
                .GetByEmailAsync(request.Email, cancellationToken);

            if (user == null)
            {
                return ServiceResult<User>.Failure("Invalid email or password", HttpStatusCode.Unauthorized);
            }

            if (!passwordHasher.VerifyHashedPassword(user.PasswordHash, request.Password))
            {
                return ServiceResult<User>.Failure("Invalid email or password", HttpStatusCode.Unauthorized);
            }

            // Rehash password if it's not in BCrypt format
            if (!user.PasswordHash.StartsWith("$2"))
            {
                user.PasswordHash = passwordHasher.HashPassword(request.Password);
                unitOfWork.UserRepository.Update(user);
                await unitOfWork.CommitAsync(cancellationToken);
            }

            return ServiceResult<User>.Success(user);
        }
    }
    
    
}

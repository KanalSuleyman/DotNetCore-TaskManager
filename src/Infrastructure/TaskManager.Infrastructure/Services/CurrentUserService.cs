using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Enums;

namespace TaskManager.Infrastructure.Services
{
    public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
    {
        public Guid? UserId => GetClaimValue(ClaimTypes.NameIdentifier) is { } id
            ? Guid.Parse(id)
            : null;

        public string? Email => GetClaimValue(ClaimTypes.Email);

        public List<ApplicationRole> Roles => httpContextAccessor.HttpContext?
            .User
            .FindAll(ClaimTypes.Role)
            .Select(c => Enum.Parse<ApplicationRole>(c.Value))
            .ToList() ?? new List<ApplicationRole>();

        public bool IsAuthenticated => httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated ?? false;

        private string? GetClaimValue(string claimType)
        {
            return httpContextAccessor.HttpContext?
                .User
                .FindFirst(claimType)?
                .Value;
        }
    }
}

using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using TaskManager.Application.Features.Authentication.Commands.Register;
using TaskManager.Application;
using TaskManager.Application.Features.Authentication.Commands.Login;
using TaskManager.Application.Interfaces;

namespace TaskManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IMediator mediator, ITokenService tokenService) : CustomBaseController
    {
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterCommand command) => CreateActionResult(await mediator.Send(command));

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginCommand command)
        {
            var result = await mediator.Send(command);
            return result.IsSuccess
                ? Ok(new { Token = tokenService.GenerateToken(result.Data!) })
                : Unauthorized(result.ErrorMessage);
        }

    }
}

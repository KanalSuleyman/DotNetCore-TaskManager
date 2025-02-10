using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskManager.Application.Features.TaskItem.Commands.CreateTaskItem;
using TaskManager.Application.Features.TaskItem.Commands.DeleteTaskItem;
using TaskManager.Application.Features.TaskItem.Commands.UpdateTaskItem;
using TaskManager.Application.Features.TaskItem.Queries.GetAllTaskItems;
using TaskManager.Application.Features.TaskItem.Queries.GetTaskItem;
using TaskManager.Application.Features.TaskItem.Queries.GetTasksByUser;
using TaskManager.Application.Interfaces;

namespace TaskManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskItemController(IMediator mediator, ICurrentUserService currentUser) : CustomBaseController
    {
        [Authorize]
        [HttpGet("user-tasks")]
        public async Task<IActionResult> GetUserTasks()
        {
            if (!currentUser.IsAuthenticated)
            {
                return Unauthorized("Log in to add or list Tasks");
            }

            var result = await mediator.Send(new GetTasksByUserQuery(currentUser.UserId!.Value));
            return Ok(result.Data);
        }

        [HttpGet("all")]
        [Authorize(Policy = "Management")]
        public async Task<IActionResult> GetAllTasks() =>
            CreateActionResult(await mediator.Send(new GetAllTaskItemsQuery()));

        [HttpGet("{id}")]
        [Authorize(Policy = "Management")]
        public async Task<IActionResult> GetTaskById(Guid id) =>
            CreateActionResult(await mediator.Send(new GetTaskItemQuery(id)));

        [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> CreateTask([FromBody] CreateTaskItemCommand command)
        {
            if (!currentUser.IsAuthenticated)
            {
                return Unauthorized("Log in to add or list Tasks");
            }

            command.UserId = currentUser.UserId!.Value;
            return CreateActionResult(await mediator.Send(command));
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateTask([FromBody] UpdateTaskItemCommand command)
        {
            return CreateActionResult(await mediator.Send(command));
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(Guid id) =>
            CreateActionResult(await mediator.Send(new DeleteTaskItemCommand(id)));
        
    }
}

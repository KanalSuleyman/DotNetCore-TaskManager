using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TaskManager.Application.Features.TaskItem.DTOs;

namespace TaskManager.Application.Features.TaskItem.Queries.GetTasksByUser
{
    public sealed record GetTasksByUserQuery(Guid UserId)
        : IRequest<ServiceResult<List<TaskResponse>>>;
}

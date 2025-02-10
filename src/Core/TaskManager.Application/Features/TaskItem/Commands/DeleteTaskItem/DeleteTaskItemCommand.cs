using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Application.Features.TaskItem.Commands.DeleteTaskItem
{
    public sealed record DeleteTaskItemCommand(Guid Id) : IRequest<ServiceResult<Unit>>;
}

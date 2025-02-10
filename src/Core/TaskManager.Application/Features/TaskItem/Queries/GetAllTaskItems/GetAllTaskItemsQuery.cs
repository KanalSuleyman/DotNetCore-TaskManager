using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Application.Features.TaskItem.DTOs;

namespace TaskManager.Application.Features.TaskItem.Queries.GetAllTaskItems
{
    public class GetAllTaskItemsQuery : IRequest<ServiceResult<List<TaskItemDto>>>
    {
    }
}

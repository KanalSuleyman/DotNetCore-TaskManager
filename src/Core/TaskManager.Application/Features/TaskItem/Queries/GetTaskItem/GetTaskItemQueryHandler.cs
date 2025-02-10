using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Application.Features.TaskItem.DTOs;
using TaskManager.Application.Interfaces;

namespace TaskManager.Application.Features.TaskItem.Queries.GetTaskItem
{
    public class GetTaskItemQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<GetTaskItemQuery, ServiceResult<TaskItemDto>>
    {
        public async Task<ServiceResult<TaskItemDto>> Handle(GetTaskItemQuery request, CancellationToken cancellationToken)
        {
            var taskItem = await unitOfWork.TaskItemRepository.GetByIdAsync(request.Id, cancellationToken);

            if (taskItem == null) return ServiceResult<TaskItemDto>.Failure("Task not found", HttpStatusCode.NotFound);

            var taskItemDto = mapper.Map<TaskItemDto>(taskItem);

            return ServiceResult<TaskItemDto>.Success(taskItemDto);
        }
    }
}

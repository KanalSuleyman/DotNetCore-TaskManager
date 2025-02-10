using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TaskManager.Application.Features.TaskItem.DTOs;
using TaskManager.Application.Interfaces;

namespace TaskManager.Application.Features.TaskItem.Queries.GetAllTaskItems
{
    public class GetAllTaskItemsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetAllTaskItemsQuery, ServiceResult<List<TaskItemDto>>>
    {

        public async Task<ServiceResult<List<TaskItemDto>>> Handle(GetAllTaskItemsQuery request,
            CancellationToken cancellationToken)
        {
            var taskItems = await unitOfWork.TaskItemRepository.GetAllAsync(cancellationToken);
            var taskItemDtos = mapper.Map<List<TaskItemDto>>(taskItems);
            return ServiceResult<List<TaskItemDto>>.Success(taskItemDtos);
        }
    }
}

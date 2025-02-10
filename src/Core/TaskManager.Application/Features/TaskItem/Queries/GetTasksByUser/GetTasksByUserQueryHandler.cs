using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TaskManager.Application.Features.TaskItem.DTOs;
using TaskManager.Application.Interfaces;

namespace TaskManager.Application.Features.TaskItem.Queries.GetTasksByUser
{
    public class GetTasksByUserQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetTasksByUserQuery, ServiceResult<List<TaskResponse>>>
    {
        public async Task<ServiceResult<List<TaskResponse>>> Handle(
            GetTasksByUserQuery request,
            CancellationToken cancellationToken)
        {
            var tasks = await unitOfWork.TaskItemRepository.GetByUserIdAsync(request.UserId, cancellationToken);

            var response = mapper.Map<List<TaskResponse>>(tasks);
            return ServiceResult<List<TaskResponse>>.Success(response);
        }
    }
}

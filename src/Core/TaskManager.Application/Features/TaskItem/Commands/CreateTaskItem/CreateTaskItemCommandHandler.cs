using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Interfaces;

namespace TaskManager.Application.Features.TaskItem.Commands.CreateTaskItem
{
    public class CreateTaskItemCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<CreateTaskItemCommand, ServiceResult<Guid>>
    {
        public async Task<ServiceResult<Guid>> Handle(CreateTaskItemCommand request, CancellationToken cancellationToken)
        {
            var user = await unitOfWork.UserRepository.GetByIdAsync(request.UserId, cancellationToken);

            if (user == null)
            {
                return ServiceResult<Guid>.Failure("User not found", HttpStatusCode.NotFound);
            }

            var taskItem = mapper.Map<Domain.Entities.TaskItem>(request);

            await unitOfWork.TaskItemRepository.AddAsync(taskItem, cancellationToken);


            await unitOfWork.CommitAsync(cancellationToken);

            return ServiceResult<Guid>.SuccessAsCreated(taskItem.Id, $"/api/tasks/{taskItem.Id}");
        }
    }
}

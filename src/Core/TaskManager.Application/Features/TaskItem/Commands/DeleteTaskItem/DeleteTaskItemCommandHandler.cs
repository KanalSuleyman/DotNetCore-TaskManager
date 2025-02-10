using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Application.Interfaces;

namespace TaskManager.Application.Features.TaskItem.Commands.DeleteTaskItem
{
    public class DeleteTaskItemCommandHandler(IUnitOfWork unitOfWork)
        : IRequestHandler<DeleteTaskItemCommand, ServiceResult<Unit>>
    {
        public async Task<ServiceResult<Unit>> Handle(DeleteTaskItemCommand request, CancellationToken cancellationToken)
        {
            var taskItem = await unitOfWork.TaskItemRepository.GetByIdAsync(request.Id, cancellationToken);

            if (taskItem == null) return ServiceResult<Unit>.Failure("Task item not found", HttpStatusCode.NotFound);

            unitOfWork.TaskItemRepository.Delete(taskItem);

            await unitOfWork.CommitAsync(cancellationToken);

            return ServiceResult<Unit>.Success(Unit.Value, HttpStatusCode.NoContent);
        }
    }
}

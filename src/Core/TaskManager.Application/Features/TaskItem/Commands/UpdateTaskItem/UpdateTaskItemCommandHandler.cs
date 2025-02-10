using System.Net;
using AutoMapper;
using MediatR;
using TaskManager.Application.Interfaces;

namespace TaskManager.Application.Features.TaskItem.Commands.UpdateTaskItem;

public class UpdateTaskItemCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<UpdateTaskItemCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(UpdateTaskItemCommand request, CancellationToken cancellationToken)
    {
        // Fetch the existing task item
        var taskItem = await unitOfWork.TaskItemRepository.GetByIdAsync(request.Id, cancellationToken);

        if (taskItem == null) return ServiceResult.Failure("Task not found", HttpStatusCode.NotFound);

        mapper.Map(request, taskItem);

        unitOfWork.TaskItemRepository.Update(taskItem);

        await unitOfWork.CommitAsync(cancellationToken);

        return ServiceResult.Success(HttpStatusCode.NoContent);
    }
}
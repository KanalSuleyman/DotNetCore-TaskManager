using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TaskManager.Application.Features.TaskItem.DTOs;

namespace TaskManager.Application.Features.TaskItem.Queries.GetTasksByUser
{
    public class GetTasksByUserQueryMappingProfile : Profile
    {
        public GetTasksByUserQueryMappingProfile()
        {
            CreateMap<Domain.Entities.TaskItem, TaskResponse>();
        }
    }
}

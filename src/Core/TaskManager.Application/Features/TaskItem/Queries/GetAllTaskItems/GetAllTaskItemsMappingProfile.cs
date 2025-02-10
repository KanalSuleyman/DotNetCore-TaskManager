using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TaskManager.Application.Features.TaskItem.DTOs;

namespace TaskManager.Application.Features.TaskItem.Queries.GetAllTaskItems
{
    public class GetAllTaskItemsMappingProfile : Profile
    {
        public GetAllTaskItemsMappingProfile()
        {
            CreateMap<Domain.Entities.TaskItem, TaskItemDto>()
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.DueDateRange!.StartDate))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.DueDateRange!.EndDate))
                .ForMember(dest => dest.Frequency, opt => opt.MapFrom(src => src.Frequency))
                .ForMember(dest => dest.PriorityLevel, opt => opt.MapFrom(src => src.TaskPriority!.PriorityLevel))
                .ReverseMap();
        }
    }
}

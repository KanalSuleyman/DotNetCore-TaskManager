using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TaskManager.Application.Features.TaskItem.DTOs;
using TaskManager.Domain.Enums;
using TaskManager.Domain.ValueObjects;

namespace TaskManager.Application.Features.TaskItem.Commands.UpdateTaskItem
{
    public class UpdateTaskItemMappingProfile : Profile
    {
        public UpdateTaskItemMappingProfile()
        {
            CreateMap<UpdateTaskItemCommand, Domain.Entities.TaskItem>()
                .ForMember(dest => dest.DueDateRange,
                    opt => opt.MapFrom(src => new DueDateRange(src.StartDate, src.EndDate)))
                .ForMember(dest => dest.Frequency,
                    opt => opt.MapFrom(src => src.Frequency))  // 
                .ForMember(dest => dest.TaskPriority,
                    opt => opt.MapFrom(src => new TaskPriority(src.PriorityLevel)))
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<Domain.Entities.TaskItem, TaskItemDto>()
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.DueDateRange!.StartDate))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.DueDateRange!.EndDate))
                .ForMember(dest => dest.Frequency, opt => opt.MapFrom(src => src.Frequency))
                .ForMember(dest => dest.PriorityLevel, opt => opt.MapFrom(src => src.TaskPriority!.PriorityLevel))
                .ReverseMap();
        }
    }
}

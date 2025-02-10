using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.Features.Authentication.Commands.Register
{
    public class RegisterCommandMappingProfile : Profile
    {
        public RegisterCommandMappingProfile()
        {
            CreateMap<RegisterCommand, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());


        }
    }
}

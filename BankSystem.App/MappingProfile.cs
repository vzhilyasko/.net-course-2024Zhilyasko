using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BankSystem.App.Dto;
using BankSystem.Models;

namespace BankSystem.App
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Client, ClientDto>()
                .ForMember(dest => dest.FullName, 
                    opt => opt.MapFrom(src => src.FirstName + " " + src.LastName + " " + src.MidlleName));

            CreateMap<ClientDto, Client>()
                .ForMember(dest => dest.FirstName, 
                    opt => opt.MapFrom(src => src.FullName.Split(" ", StringSplitOptions.None)[0]));
            CreateMap<ClientDto, Client>()
                .ForMember(dest => dest.LastName,
                    opt => opt.MapFrom(src => src.FullName.Split(" ", StringSplitOptions.None)[1]));
            CreateMap<ClientDto, Client>()
                .ForMember(dest => dest.MidlleName,
                    opt => opt.MapFrom(src => src.FullName.Split(" ", StringSplitOptions.None)[2]));


            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.FullName,
                    opt => opt.MapFrom(src => src.FirstName + " " + src.LastName + " " + src.MidlleName));

            CreateMap<EmployeeDto, Employee>()
                .ForMember(dest => dest.FirstName,
                    opt => opt.MapFrom(src => src.FullName.Split(" ", StringSplitOptions.None)[0]));
            CreateMap<EmployeeDto, Employee>()
                .ForMember(dest => dest.LastName,
                    opt => opt.MapFrom(src => src.FullName.Split(" ", StringSplitOptions.None)[1]));
            CreateMap<EmployeeDto, Employee>()
                .ForMember(dest => dest.MidlleName,
                    opt => opt.MapFrom(src => src.FullName.Split(" ", StringSplitOptions.None)[2]));



        }
    }
}

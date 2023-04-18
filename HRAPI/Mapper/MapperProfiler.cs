using AutoMapper;
using HRAPI.Entities;
using HRAPI.Models;

namespace Lemondo.Mapper
{
    public class MapperProfiler : Profile
    {
        public MapperProfiler()
        {
            CreateMap<AdministratorEntity, Administrator>().ReverseMap();
            CreateMap<AdministratorEntity, Administrator>().ReverseMap()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.ToLower()));

            CreateMap<Administrator, EmployeeEntity>().ReverseMap()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.ToLower()))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName.ToLower()))
                .ForMember(dest => dest.JobTitle, opt => opt.MapFrom(src => src.JobTitle.ToLower()));
            
            CreateMap<EmployeeEntity, Employee>().ReverseMap();
            CreateMap<EmployeeEntity, Employee>().ReverseMap()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.ToLower()))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName.ToLower()))
                .ForMember(dest => dest.JobTitle, opt => opt.MapFrom(src => src.JobTitle.ToLower()));


        }
    }
}

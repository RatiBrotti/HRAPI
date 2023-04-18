using AutoMapper;
using HRAPI.Entities;
using HRAPI.Models;

namespace HRAPI.Mapper
{
    public class MapperProfiler : Profile
    {
        public MapperProfiler()
        {
            CreateMap<Administrator, AdministratorModel>().ReverseMap();
            CreateMap<Administrator, AdministratorModel>().ReverseMap()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.ToLower()));

            CreateMap<AdministratorModel, Employee>().ReverseMap()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.ToLower()))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName.ToLower()))
                .ForMember(dest => dest.JobTitle, opt => opt.MapFrom(src => src.JobTitle.ToLower()));

            CreateMap<Employee, AdministratorModel>().ReverseMap()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.ToLower()))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName.ToLower()))
                .ForMember(dest => dest.JobTitle, opt => opt.MapFrom(src => src.JobTitle.ToLower()));

            CreateMap<Employee, EmployeeModel>().ReverseMap();
            CreateMap<Employee, EmployeeModel>().ReverseMap()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.ToLower()))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName.ToLower()))
                .ForMember(dest => dest.JobTitle, opt => opt.MapFrom(src => src.JobTitle.ToLower()));


        }
    }
}

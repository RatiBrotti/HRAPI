using AutoMapper;
using HRMVC.Models;

namespace Lemondo.Mapper
{
    public class MapperProfiler : Profile
    {
        public MapperProfiler()
        {
            CreateMap<Administrator, Administrator>().ReverseMap();
            CreateMap<Administrator, Administrator>().ReverseMap()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.ToLower()))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName.ToLower()))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.ToLower()));
            
            CreateMap<Employee, Employee>().ReverseMap();
            CreateMap<Employee, Employee>().ReverseMap()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.ToLower()))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName.ToLower()))
                .ForMember(dest => dest.JobTitle, opt => opt.MapFrom(src => src.JobTitle.ToLower()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToLower()));


        }
    }
}

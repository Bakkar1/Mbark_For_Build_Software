using AutoMapper;
using DataAccessLayer.DTOs;
using DataAccessLayer.Model;

namespace DataAccessLayer.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Domain to DTO
        CreateMap<ConstructionSite, ConstructionSiteDTO>();
        CreateMap<Employee, EmployeeDTO>();
        CreateMap<ConstructionSite, CreateConstructionSiteDTO>();
        CreateMap<Employee, CreateEmployeeDTO>();

        // DTO to Domain
        CreateMap<ConstructionSiteDTO, ConstructionSite>();
        CreateMap<EmployeeDTO, Employee>();
        CreateMap<CreateConstructionSiteDTO, ConstructionSite>();
        CreateMap<CreateEmployeeDTO, Employee>();
    }
}

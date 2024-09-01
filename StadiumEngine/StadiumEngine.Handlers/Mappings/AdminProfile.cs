using AutoMapper;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.DTO.Admin;

namespace StadiumEngine.Handlers.Mappings;

internal class AdminProfile : Profile
{
    public AdminProfile()
    {
        CreateMap<StadiumGroup, StadiumGroupDto>();
    }
}
using AutoMapper;
using Unfield.Domain.Entities.Accounts;
using Unfield.DTO.Admin;

namespace Unfield.Handlers.Mappings;

internal class AdminProfile : Profile
{
    public AdminProfile()
    {
        CreateMap<StadiumGroup, StadiumGroupDto>();
    }
}
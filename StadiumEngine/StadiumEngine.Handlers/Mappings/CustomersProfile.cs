using System.Security.Claims;
using AutoMapper;
using StadiumEngine.Commands.Customers;
using StadiumEngine.Domain.Entities.Customers;
using StadiumEngine.Domain.Services.Models.Customers;
using StadiumEngine.DTO.Customers;
using StadiumEngine.DTO.Schedule;

namespace StadiumEngine.Handlers.Mappings;

internal class CustomersProfile : Profile
{
    public CustomersProfile()
    {
        CreateMap<RegisterCustomerCommandBody, CreateCustomerData>();
        CreateMap<Customer, AuthorizedCustomerBaseDto>();
        
        CreateMap<Customer, AuthorizedCustomerDto>()
            .IncludeBase<Customer, AuthorizedCustomerBaseDto>()
            .ForMember(
                dest => dest.Stadiums,
                act => act.MapFrom( s => s.StadiumGroup.Stadiums ) );

        CreateMap<Customer, AuthorizeCustomerDto>()
            .IncludeBase<Customer, AuthorizedCustomerBaseDto>()
            .ForMember(
                dest => dest.Claims,
                act => act.MapFrom( s => CreateClaimsList( s ) ) );

        CreateMap<BookingListItemDto, CustomerBookingListItemDto>();
    }

    private List<Claim> CreateClaimsList( Customer customer )
    {
        List<Claim> claims = new()
        {
            new Claim( "customerId", customer.Id.ToString() ),
            new Claim( "customerStadiumGroupId", customer.StadiumGroupId.ToString() ),
            new Claim( "customerPhoneNumber", customer.PhoneNumber )
        };

        return claims;
    }
}
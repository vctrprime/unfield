using System.Security.Claims;
using AutoMapper;
using StadiumEngine.Domain.Entities.Customers;
using StadiumEngine.DTO.Customers;

namespace StadiumEngine.Handlers.Mappings;

internal class CustomersProfile : Profile
{
    public CustomersProfile()
    {
        CreateMap<Customer, AuthorizedCustomerDto>();
        CreateMap<Customer, AuthorizeCustomerDto>() 
            .IncludeBase<Customer, AuthorizedCustomerDto>()
            .ForMember(
                dest => dest.Claims,
                act => act.MapFrom( s => CreateClaimsList( s ) ) );
    }
    
    private List<Claim> CreateClaimsList( Customer customer )
    {
        List<Claim> claims = new()
        {
            new Claim( "customerId", customer.Id.ToString() ),
            new Claim( "customerPhoneNumber", customer.PhoneNumber )
        };

        return claims;
    }
}
using AutoMapper;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Entities.Customers;
using StadiumEngine.Domain.Services.Core.Customers;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Customers;
using StadiumEngine.Queries.Customers;

namespace StadiumEngine.Handlers.Handlers.Customers;

internal sealed class GetAuthorizedCustomerHandler : BaseCustomerRequestHandler<GetAuthorizedCustomerQuery, AuthorizedCustomerDto>
{
    private readonly ICustomerQueryService _queryService;
    public GetAuthorizedCustomerHandler( 
        IMapper mapper, 
        ICustomerQueryService queryService,
        ICustomerClaimsIdentityService claimsIdentityService ) : base( mapper, claimsIdentityService )
    {
        _queryService = queryService;
    }

    public override async ValueTask<AuthorizedCustomerDto> Handle( GetAuthorizedCustomerQuery request,
        CancellationToken cancellationToken )
    {
        Customer? customer = await _queryService.GetCustomerAsync( _customerId );

        AuthorizedCustomerDto? customerDto = Mapper.Map<AuthorizedCustomerDto>( customer );

        return customerDto;
    }
}
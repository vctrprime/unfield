using AutoMapper;
using Unfield.Domain.Entities.Accounts;
using Unfield.Domain.Entities.Customers;
using Unfield.Domain.Services.Core.Customers;
using Unfield.Domain.Services.Identity;
using Unfield.DTO.Customers;
using Unfield.Queries.Customers;

namespace Unfield.Handlers.Handlers.Customers;

internal sealed class GetAuthorizedCustomerHandler : BaseCustomerRequestHandler<GetAuthorizedCustomerQuery, AuthorizedCustomerDto?>
{
    private readonly ICustomerQueryService _queryService;
    public GetAuthorizedCustomerHandler( 
        IMapper mapper, 
        ICustomerQueryService queryService,
        ICustomerClaimsIdentityService claimsIdentityService ) : base( mapper, claimsIdentityService )
    {
        _queryService = queryService;
    }

    public override async ValueTask<AuthorizedCustomerDto?> Handle( GetAuthorizedCustomerQuery request,
        CancellationToken cancellationToken )
    {
        if ( _customerId == 0 )
        {
            return null;
        }
        
        Customer? customer = await _queryService.GetCustomerAsync( _customerId );

        AuthorizedCustomerDto? customerDto = Mapper.Map<AuthorizedCustomerDto>( customer );

        return customerDto;
    }
}
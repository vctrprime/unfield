using AutoMapper;
using Unfield.Common;
using Unfield.Common.Exceptions;
using Unfield.Domain.Entities.Accounts;
using Unfield.Domain.Services.Core.Accounts;
using Unfield.DTO.Accounts.Stadiums;
using Unfield.Queries.Customers;

namespace Unfield.Handlers.Handlers.Customers;

internal sealed class GetStadiumHandler : BaseCustomerRequestHandler<GetStadiumQuery, StadiumDto>
{
    private readonly IStadiumQueryService _stadiumQueryService;

    public GetStadiumHandler( 
        IMapper mapper, 
        IStadiumQueryService stadiumQueryService ) : base( mapper, null )
    {
        _stadiumQueryService = stadiumQueryService;
    }

    public override async ValueTask<StadiumDto> Handle( GetStadiumQuery request, CancellationToken cancellationToken )
    {
        Stadium? stadium = await _stadiumQueryService.GetAsync( request.StadiumToken );

        if ( stadium is null )
        {
            throw new DomainException( ErrorsKeys.StadiumNotFound );
        }
        
        StadiumDto dto = Mapper.Map<StadiumDto>( stadium );
        return dto;
    }
}
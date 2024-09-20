using AutoMapper;
using StadiumEngine.Common;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Services.Core.Accounts;
using StadiumEngine.DTO.Accounts.Stadiums;
using StadiumEngine.Queries.Customers;

namespace StadiumEngine.Handlers.Handlers.Customers;

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
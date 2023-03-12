using AutoMapper;
using StadiumEngine.Domain.Entities.Settings;
using StadiumEngine.Domain.Services.Facades.Settings;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Settings.Stadiums;
using StadiumEngine.Handlers.Queries.Settings.Stadiums;

namespace StadiumEngine.Handlers.Handlers.Settings.Stadiums;

internal sealed class GetStadiumMainSettingsHandler : BaseRequestHandler<GetStadiumMainSettingsQuery, StadiumMainSettingsDto>
{
    private readonly IStadiumMainSettingsQueryFacade _mainSettingsFacade;

    public GetStadiumMainSettingsHandler(
        IStadiumMainSettingsQueryFacade mainSettingsFacade,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService ) : base( mapper, claimsIdentityService )
    {
        _mainSettingsFacade = mainSettingsFacade;
    }

    public override async ValueTask<StadiumMainSettingsDto> Handle( GetStadiumMainSettingsQuery request,
        CancellationToken cancellationToken )
    {
        StadiumMainSettings mainSettings = await _mainSettingsFacade.GetByStadiumId( _currentStadiumId );
        
        StadiumMainSettingsDto mainSettingsDto = Mapper.Map<StadiumMainSettingsDto>( mainSettings );

        return mainSettingsDto;
    }
}
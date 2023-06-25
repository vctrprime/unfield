using AutoMapper;
using StadiumEngine.Domain.Entities.Settings;
using StadiumEngine.Domain.Services.Facades.Settings;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Settings.Main;
using StadiumEngine.Queries.Settings.Main;

namespace StadiumEngine.Handlers.Handlers.Settings.Main;

internal sealed class GetMainSettingsHandler : BaseRequestHandler<GetMainSettingsQuery, MainSettingsDto>
{
    private readonly IMainSettingsQueryFacade _mainSettingsFacade;

    public GetMainSettingsHandler(
        IMainSettingsQueryFacade mainSettingsFacade,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService ) : base( mapper, claimsIdentityService )
    {
        _mainSettingsFacade = mainSettingsFacade;
    }

    public override async ValueTask<MainSettingsDto> Handle( GetMainSettingsQuery request,
        CancellationToken cancellationToken )
    {
        MainSettings mainSettings = await _mainSettingsFacade.GetByStadiumIdAsync( _currentStadiumId );
        
        MainSettingsDto mainSettingsDto = Mapper.Map<MainSettingsDto>( mainSettings );

        return mainSettingsDto;
    }
}
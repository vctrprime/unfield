using AutoMapper;
using StadiumEngine.Domain.Entities.Settings;
using StadiumEngine.Domain.Services.Core.Settings;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Settings.Main;
using StadiumEngine.Queries.Settings.Main;

namespace StadiumEngine.Handlers.Handlers.Settings.Main;

internal sealed class GetMainSettingsHandler : BaseRequestHandler<GetMainSettingsQuery, MainSettingsDto>
{
    private readonly IMainSettingsQueryService _queryService;

    public GetMainSettingsHandler(
        IMainSettingsQueryService queryService,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService ) : base( mapper, claimsIdentityService )
    {
        _queryService = queryService;
    }

    public override async ValueTask<MainSettingsDto> Handle(
        GetMainSettingsQuery request,
        CancellationToken cancellationToken )
    {
        MainSettings mainSettings = await _queryService.GetByStadiumIdAsync(
            _currentStadiumId == 0 && request.StadiumId.HasValue ? request.StadiumId.Value : _currentStadiumId );

        MainSettingsDto mainSettingsDto = Mapper.Map<MainSettingsDto>( mainSettings );

        return mainSettingsDto;
    }
}
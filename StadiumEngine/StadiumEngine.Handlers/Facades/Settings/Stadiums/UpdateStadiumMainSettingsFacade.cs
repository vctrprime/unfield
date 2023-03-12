using StadiumEngine.Domain.Entities.Settings;
using StadiumEngine.Domain.Services.Facades.Settings;
using StadiumEngine.DTO.Settings.Stadiums;
using StadiumEngine.Commands.Settings.Stadiums;
using StadiumEngine.Common;
using StadiumEngine.Common.Exceptions;

namespace StadiumEngine.Handlers.Facades.Settings.Stadiums;

internal class UpdateStadiumMainSettingsFacade : IUpdateStadiumMainSettingsFacade
{
    private readonly IStadiumMainSettingsCommandFacade _commandFacade;
    private readonly IStadiumMainSettingsQueryFacade _queryFacade;

    public UpdateStadiumMainSettingsFacade( IStadiumMainSettingsQueryFacade queryFacade,
        IStadiumMainSettingsCommandFacade commandFacade )
    {
        _queryFacade = queryFacade;
        _commandFacade = commandFacade;
    }

    public async Task<UpdateStadiumMainSettingsDto> Update( UpdateStadiumMainSettingsCommand request, int stadiumId,
        int userId )
    {
        Validate( request );

        StadiumMainSettings mainSettings = await _queryFacade.GetByStadiumId( stadiumId );

        mainSettings.Name = request.Name;
        mainSettings.Description = request.Description;
        mainSettings.OpenTime = request.OpenTime;
        mainSettings.CloseTime = request.CloseTime;
        mainSettings.UserModifiedId = userId;

        _commandFacade.UpdateMainSettings( mainSettings );

        return new UpdateStadiumMainSettingsDto();
    }

    private static void Validate( UpdateStadiumMainSettingsCommand request )
    {
        if ( request.OpenTime < 0 ||
             request.CloseTime > 24 ||
             request.OpenTime >= request.CloseTime )
        {
            throw new DomainException( ErrorsKeys.MainSettingsInvalidOpenClosePeriod );
        }
    }
}
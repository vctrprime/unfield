using StadiumEngine.Commands.Settings.Main;
using StadiumEngine.Common;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Entities.Settings;
using StadiumEngine.Domain.Services.Facades.Settings;
using StadiumEngine.DTO.Settings.Main;

namespace StadiumEngine.Handlers.Facades.Settings.Main;

internal class UpdateMainSettingsFacade : IUpdateMainSettingsFacade
{
    private readonly IMainSettingsCommandFacade _commandFacade;
    private readonly IMainSettingsQueryFacade _queryFacade;

    public UpdateMainSettingsFacade( IMainSettingsQueryFacade queryFacade,
        IMainSettingsCommandFacade commandFacade )
    {
        _queryFacade = queryFacade;
        _commandFacade = commandFacade;
    }

    public async Task<UpdateMainSettingsDto> UpdateAsync( UpdateMainSettingsCommand request, int stadiumId,
        int userId )
    {
        Validate( request );

        MainSettings mainSettings = await _queryFacade.GetByStadiumIdAsync( stadiumId );

        mainSettings.Name = request.Name;
        mainSettings.Description = request.Description;
        mainSettings.OpenTime = request.OpenTime;
        mainSettings.CloseTime = request.CloseTime;
        mainSettings.UserModifiedId = userId;

        _commandFacade.UpdateMainSettings( mainSettings );

        return new UpdateMainSettingsDto();
    }

    private static void Validate( UpdateMainSettingsCommand request )
    {
        if ( request.OpenTime < 0 ||
             request.CloseTime > 24 ||
             request.OpenTime >= request.CloseTime )
        {
            throw new DomainException( ErrorsKeys.MainSettingsInvalidOpenClosePeriod );
        }
    }
}
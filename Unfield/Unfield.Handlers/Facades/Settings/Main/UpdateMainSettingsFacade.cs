using Unfield.Commands.Settings.Main;
using Unfield.Common;
using Unfield.Common.Exceptions;
using Unfield.Domain.Entities.Settings;
using Unfield.Domain.Services.Core.Settings;
using Unfield.DTO.Settings.Main;

namespace Unfield.Handlers.Facades.Settings.Main;

internal class UpdateMainSettingsFacade : IUpdateMainSettingsFacade
{
    private readonly IMainSettingsCommandService _commandService;
    private readonly IMainSettingsQueryService _queryService;

    public UpdateMainSettingsFacade( IMainSettingsQueryService queryService,
        IMainSettingsCommandService commandService )
    {
        _queryService = queryService;
        _commandService = commandService;
    }

    public async Task<UpdateMainSettingsDto> UpdateAsync( UpdateMainSettingsCommand request, int stadiumId,
        int userId )
    {
        Validate( request );

        MainSettings mainSettings = await _queryService.GetByStadiumIdAsync( stadiumId );

        mainSettings.Name = request.Name;
        mainSettings.Description = request.Description;
        mainSettings.OpenTime = request.OpenTime;
        mainSettings.CloseTime = request.CloseTime;
        mainSettings.UserModifiedId = userId;

        _commandService.UpdateMainSettings( mainSettings );

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
using AutoMapper;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Settings.Stadiums;
using StadiumEngine.Handlers.Commands.Settings.Stadiums;
using StadiumEngine.Handlers.Facades.Settings.Stadiums;

namespace StadiumEngine.Handlers.Handlers.Settings.Stadiums;

internal sealed class UpdateStadiumMainSettingsHandler : BaseCommandHandler<UpdateStadiumMainSettingsCommand, UpdateStadiumMainSettingsDto>
{
    private readonly IUpdateStadiumMainSettingsFacade _facade;

    public UpdateStadiumMainSettingsHandler(
        IUpdateStadiumMainSettingsFacade facade,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService,
        IUnitOfWork unitOfWork ) : base( mapper, claimsIdentityService, unitOfWork )
    {
        _facade = facade;
    }

    protected override async ValueTask<UpdateStadiumMainSettingsDto> HandleCommand( UpdateStadiumMainSettingsCommand request,
        CancellationToken cancellationToken ) =>
        await _facade.Update( request, _currentStadiumId, _userId );
}
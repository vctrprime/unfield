using AutoMapper;
using StadiumEngine.Commands.Settings.Main;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Settings.Main;
using StadiumEngine.Handlers.Facades.Settings.Main;

namespace StadiumEngine.Handlers.Handlers.Settings.Main;

internal sealed class UpdateMainSettingsHandler : BaseCommandHandler<UpdateMainSettingsCommand, UpdateMainSettingsDto>
{
    private readonly IUpdateMainSettingsFacade _facade;

    public UpdateMainSettingsHandler(
        IUpdateMainSettingsFacade facade,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService,
        IUnitOfWork unitOfWork ) : base( mapper, claimsIdentityService, unitOfWork )
    {
        _facade = facade;
    }

    protected override async ValueTask<UpdateMainSettingsDto> HandleCommandAsync( UpdateMainSettingsCommand request,
        CancellationToken cancellationToken ) =>
        await _facade.UpdateAsync( request, _currentStadiumId, _userId );
}
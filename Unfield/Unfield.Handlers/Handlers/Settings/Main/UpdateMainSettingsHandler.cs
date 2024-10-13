using AutoMapper;
using Unfield.Commands.Settings.Main;
using Unfield.Domain;
using Unfield.Domain.Services.Identity;
using Unfield.DTO.Settings.Main;
using Unfield.Handlers.Facades.Settings.Main;

namespace Unfield.Handlers.Handlers.Settings.Main;

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
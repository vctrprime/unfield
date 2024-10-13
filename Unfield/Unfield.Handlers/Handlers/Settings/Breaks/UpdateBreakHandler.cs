using AutoMapper;
using Unfield.Commands.Settings.Breaks;
using Unfield.Domain;
using Unfield.Domain.Services.Identity;
using Unfield.DTO.Settings.Breaks;
using Unfield.Handlers.Facades.Settings.Breaks;

namespace Unfield.Handlers.Handlers.Settings.Breaks;

internal sealed class UpdateBreakHandler : BaseCommandHandler<UpdateBreakCommand, UpdateBreakDto>
{
    private readonly IUpdateBreakFacade _facade;

    public UpdateBreakHandler(
        IUpdateBreakFacade facade,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService,
        IUnitOfWork unitOfWork ) : base( mapper, claimsIdentityService, unitOfWork )
    {
        _facade = facade;
    }

    protected override async ValueTask<UpdateBreakDto> HandleCommandAsync( UpdateBreakCommand request,
        CancellationToken cancellationToken ) =>
        await _facade.UpdateAsync(
            request,
            _currentStadiumId,
            _userId );
}
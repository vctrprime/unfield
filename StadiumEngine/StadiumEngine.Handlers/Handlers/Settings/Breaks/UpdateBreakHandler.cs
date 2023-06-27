using AutoMapper;
using StadiumEngine.Commands.Settings.Breaks;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Settings.Breaks;
using StadiumEngine.Handlers.Facades.Settings.Breaks;

namespace StadiumEngine.Handlers.Handlers.Settings.Breaks;

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
using AutoMapper;
using StadiumEngine.Commands.Settings.Breaks;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Services.Facades.Settings;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Settings.Breaks;

namespace StadiumEngine.Handlers.Handlers.Settings.Breaks;

internal sealed class DeleteBreakHandler : BaseCommandHandler<DeleteBreakCommand, DeleteBreakDto>
{
    private readonly IBreakCommandFacade _breakFacade;

    public DeleteBreakHandler(
        IBreakCommandFacade breakFacade,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService,
        IUnitOfWork unitOfWork ) : base( mapper, claimsIdentityService, unitOfWork )
    {
        _breakFacade = breakFacade;
    }

    protected override async ValueTask<DeleteBreakDto> HandleCommandAsync( DeleteBreakCommand request,
        CancellationToken cancellationToken )
    {
        await _breakFacade.DeleteBreakAsync( request.BreakId, _currentStadiumId );
        return new DeleteBreakDto();
    }
}
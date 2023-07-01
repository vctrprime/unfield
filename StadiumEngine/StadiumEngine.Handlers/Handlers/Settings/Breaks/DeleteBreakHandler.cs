using AutoMapper;
using StadiumEngine.Commands.Settings.Breaks;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Services.Core.Settings;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Settings.Breaks;

namespace StadiumEngine.Handlers.Handlers.Settings.Breaks;

internal sealed class DeleteBreakHandler : BaseCommandHandler<DeleteBreakCommand, DeleteBreakDto>
{
    private readonly IBreakCommandService _commandService;

    public DeleteBreakHandler(
        IBreakCommandService commandService,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService,
        IUnitOfWork unitOfWork ) : base( mapper, claimsIdentityService, unitOfWork )
    {
        _commandService = commandService;
    }

    protected override async ValueTask<DeleteBreakDto> HandleCommandAsync( DeleteBreakCommand request,
        CancellationToken cancellationToken )
    {
        await _commandService.DeleteBreakAsync( request.BreakId, _currentStadiumId );
        return new DeleteBreakDto();
    }
}
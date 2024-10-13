using AutoMapper;
using Unfield.Commands.Settings.Breaks;
using Unfield.Domain;
using Unfield.Domain.Services.Core.Settings;
using Unfield.Domain.Services.Identity;
using Unfield.DTO.Settings.Breaks;

namespace Unfield.Handlers.Handlers.Settings.Breaks;

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
using AutoMapper;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Services.Core.Rates;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Rates.PriceGroups;
using StadiumEngine.Commands.Rates.PriceGroups;

namespace StadiumEngine.Handlers.Handlers.Rates.PriceGroups;

internal sealed class DeletePriceGroupHandler : BaseCommandHandler<DeletePriceGroupCommand, DeletePriceGroupDto>
{
    private readonly IPriceGroupCommandService _commandService;

    public DeletePriceGroupHandler(
        IPriceGroupCommandService commandService,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService,
        IUnitOfWork unitOfWork ) : base( mapper, claimsIdentityService, unitOfWork )
    {
        _commandService = commandService;
    }

    protected override async ValueTask<DeletePriceGroupDto> HandleCommandAsync( DeletePriceGroupCommand request,
        CancellationToken cancellationToken )
    {
        await _commandService.DeletePriceGroupAsync( request.PriceGroupId, _currentStadiumId );
        return new DeletePriceGroupDto();
    }
}
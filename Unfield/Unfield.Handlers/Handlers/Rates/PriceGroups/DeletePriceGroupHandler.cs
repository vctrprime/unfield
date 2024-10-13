using AutoMapper;
using Unfield.Domain;
using Unfield.Domain.Services.Core.Rates;
using Unfield.Domain.Services.Identity;
using Unfield.DTO.Rates.PriceGroups;
using Unfield.Commands.Rates.PriceGroups;

namespace Unfield.Handlers.Handlers.Rates.PriceGroups;

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
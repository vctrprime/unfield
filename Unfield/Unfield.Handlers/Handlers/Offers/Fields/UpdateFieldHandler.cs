using AutoMapper;
using Unfield.Domain;
using Unfield.Domain.Services.Identity;
using Unfield.DTO.Offers.Fields;
using Unfield.Commands.Offers.Fields;
using Unfield.Handlers.Facades.Offers.Fields;

namespace Unfield.Handlers.Handlers.Offers.Fields;

internal sealed class UpdateFieldHandler : BaseCommandHandler<UpdateFieldCommand, UpdateFieldDto>
{
    private readonly IUpdateFieldFacade _facade;

    public UpdateFieldHandler(
        IUpdateFieldFacade facade,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService,
        IUnitOfWork unitOfWork ) : base( mapper, claimsIdentityService, unitOfWork )
    {
        _facade = facade;
    }

    protected override async ValueTask<UpdateFieldDto> HandleCommandAsync( UpdateFieldCommand request,
        CancellationToken cancellationToken ) =>
        await _facade.UpdateAsync( request, _currentStadiumId, _userId );
}
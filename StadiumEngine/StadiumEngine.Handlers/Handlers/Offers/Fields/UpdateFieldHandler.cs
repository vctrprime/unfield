using AutoMapper;
using StadiumEngine.Common;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Offers.Fields;
using StadiumEngine.Handlers.Commands.Offers.Fields;
using StadiumEngine.Handlers.Facades.Offers.Fields;

namespace StadiumEngine.Handlers.Handlers.Offers.Fields;

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

    protected override async ValueTask<UpdateFieldDto> HandleCommand( UpdateFieldCommand request,
        CancellationToken cancellationToken )
    {
        return await _facade.Update( request, _currentStadiumId, _userId );
    }
}
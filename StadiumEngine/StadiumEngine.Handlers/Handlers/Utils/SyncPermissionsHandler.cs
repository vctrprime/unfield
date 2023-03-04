using AutoMapper;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Services;
using StadiumEngine.Domain.Services.Facades.Accounts;
using StadiumEngine.DTO.Utils;
using StadiumEngine.Handlers.Commands.Utils;

namespace StadiumEngine.Handlers.Handlers.Utils;

internal sealed class SyncPermissionsHandler : BaseCommandHandler<SyncPermissionsCommand, SyncPermissionsDto>
{
    private readonly IPermissionFacade _permissionFacade;
    
    public SyncPermissionsHandler(
        IPermissionFacade permissionFacade,
        IMapper mapper, 
        IUnitOfWork unitOfWork) : base(mapper, null, unitOfWork, false)
    {
        _permissionFacade = permissionFacade;
    }

    protected override async ValueTask<SyncPermissionsDto> HandleCommand(SyncPermissionsCommand request, CancellationToken cancellationToken)
    {
        await _permissionFacade.Sync(UnitOfWork);
        return new SyncPermissionsDto();
    }
}
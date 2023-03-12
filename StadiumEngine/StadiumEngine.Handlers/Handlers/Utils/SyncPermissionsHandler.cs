using AutoMapper;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Services.Facades.Accounts;
using StadiumEngine.DTO.Utils;
using StadiumEngine.Commands.Utils;

namespace StadiumEngine.Handlers.Handlers.Utils;

internal sealed class SyncPermissionsHandler : BaseCommandHandler<SyncPermissionsCommand, SyncPermissionsDto>
{
    private readonly IPermissionCommandFacade _permissionFacade;

    public SyncPermissionsHandler(
        IPermissionCommandFacade permissionFacade,
        IMapper mapper,
        IUnitOfWork unitOfWork ) : base(
        mapper,
        null,
        unitOfWork,
        false )
    {
        _permissionFacade = permissionFacade;
    }

    protected override async ValueTask<SyncPermissionsDto> HandleCommand( SyncPermissionsCommand request,
        CancellationToken cancellationToken )
    {
        await _permissionFacade.Sync( UnitOfWork );
        return new SyncPermissionsDto();
    }
}
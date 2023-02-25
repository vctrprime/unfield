using AutoMapper;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Services;
using StadiumEngine.Domain.Services.Facades.Accounts;
using StadiumEngine.DTO.Utils;
using StadiumEngine.Handlers.Commands.Utils;

namespace StadiumEngine.Handlers.Handlers.Utils;

internal sealed class SyncPermissionsHandler : BaseRequestHandler<SyncPermissionsCommand, SyncPermissionsDto>
{
    private readonly IPermissionFacade _permissionFacade;
    
    public SyncPermissionsHandler(
        IPermissionFacade permissionFacade,
        IMapper mapper, 
        IUnitOfWork unitOfWork) : base(mapper, null, unitOfWork)
    {
        _permissionFacade = permissionFacade;
    }

    public override async ValueTask<SyncPermissionsDto> Handle(SyncPermissionsCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await UnitOfWork.BeginTransaction();

            await _permissionFacade.Sync(UnitOfWork);
            
            await UnitOfWork.CommitTransaction();
            
            return new SyncPermissionsDto();
        }
        catch
        {
            await UnitOfWork.RollbackTransaction();
            throw;
        }
    }
}
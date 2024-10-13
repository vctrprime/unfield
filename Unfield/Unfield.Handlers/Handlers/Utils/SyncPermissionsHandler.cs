using AutoMapper;
using Unfield.Domain;
using Unfield.Domain.Services.Core.Accounts;
using Unfield.DTO.Utils;
using Unfield.Commands.Utils;

namespace Unfield.Handlers.Handlers.Utils;

internal sealed class SyncPermissionsHandler : BaseCommandHandler<SyncPermissionsCommand, SyncPermissionsDto>
{
    private readonly IPermissionCommandService _commandService;

    public SyncPermissionsHandler(
        IPermissionCommandService commandService,
        IMapper mapper,
        IUnitOfWork unitOfWork ) : base(
        mapper,
        null,
        unitOfWork,
        false )
    {
        _commandService = commandService;
    }

    protected override async ValueTask<SyncPermissionsDto> HandleCommandAsync( SyncPermissionsCommand request,
        CancellationToken cancellationToken )
    {
        await _commandService.SyncPermissionsAsync();
        return new SyncPermissionsDto();
    }
}
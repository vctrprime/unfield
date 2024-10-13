using AutoMapper;
using Unfield.Domain;
using Unfield.Domain.Entities.Accounts;
using Unfield.Domain.Services.Core.Accounts;
using Unfield.Domain.Services.Infrastructure;
using Unfield.DTO.Accounts.Users;
using Unfield.Commands.Accounts.Users;

namespace Unfield.Handlers.Handlers.Accounts.Users;

internal sealed class ResetUserPasswordHandler : BaseCommandHandler<ResetUserPasswordCommand, ResetUserPasswordDto>
{
    private readonly IUserCommandService _commandService;

    public ResetUserPasswordHandler(
        IUserCommandService commandService,
        IMapper mapper,
        IUnitOfWork unitOfWork ) : base(
        mapper,
        null,
        unitOfWork )
    {
        _commandService = commandService;
    }

    protected override async ValueTask<ResetUserPasswordDto> HandleCommandAsync( ResetUserPasswordCommand request,
        CancellationToken cancellationToken )
    {
        await _commandService.ResetPasswordAsync( request.PhoneNumber );
        return new ResetUserPasswordDto();
    }
}
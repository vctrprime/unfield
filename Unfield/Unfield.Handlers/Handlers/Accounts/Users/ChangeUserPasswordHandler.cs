using AutoMapper;
using Unfield.Common;
using Unfield.Common.Exceptions;
using Unfield.Domain;
using Unfield.Domain.Services.Core.Accounts;
using Unfield.Domain.Services.Identity;
using Unfield.DTO.Accounts.Users;
using Unfield.Commands.Accounts.Users;

namespace Unfield.Handlers.Handlers.Accounts.Users;

internal sealed class ChangeUserPasswordHandler : BaseCommandHandler<ChangeUserPasswordCommand, ChangeUserPasswordDto>
{
    private readonly IUserCommandService _commandService;

    public ChangeUserPasswordHandler(
        IUserCommandService commandService,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService,
        IUnitOfWork unitOfWork ) : base( mapper, claimsIdentityService, unitOfWork )
    {
        _commandService = commandService;
    }

    protected override async ValueTask<ChangeUserPasswordDto> HandleCommandAsync( ChangeUserPasswordCommand request,
        CancellationToken cancellationToken )
    {
        if ( request.NewPassword != request.NewPasswordRepeat )
        {
            throw new DomainException( ErrorsKeys.PasswordsNotEqual );
        }

        await _commandService.ChangePasswordAsync( _userId, request.NewPassword, request.OldPassword );

        return new ChangeUserPasswordDto();
    }
}
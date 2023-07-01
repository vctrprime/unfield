using AutoMapper;
using StadiumEngine.Common;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Services.Core.Accounts;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Accounts.Users;
using StadiumEngine.Commands.Accounts.Users;

namespace StadiumEngine.Handlers.Handlers.Accounts.Users;

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
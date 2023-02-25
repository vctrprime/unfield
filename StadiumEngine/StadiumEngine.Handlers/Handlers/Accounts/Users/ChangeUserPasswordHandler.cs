using AutoMapper;
using StadiumEngine.Common;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Services;
using StadiumEngine.Domain.Services.Facades.Accounts;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Accounts.Users;
using StadiumEngine.Handlers.Commands.Accounts.Users;

namespace StadiumEngine.Handlers.Handlers.Accounts.Users;

internal sealed class ChangeUserPasswordHandler : BaseRequestHandler<ChangeUserPasswordCommand, ChangeUserPasswordDto>
{
    private readonly IUserFacade _userFacade;

    public ChangeUserPasswordHandler(
        IUserFacade userFacade,
        IMapper mapper, 
        IClaimsIdentityService claimsIdentityService, 
        IUnitOfWork unitOfWork) : base(mapper, claimsIdentityService, unitOfWork)
    {
        _userFacade = userFacade;
    }
    
    public override async ValueTask<ChangeUserPasswordDto> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
    {
        if (request.NewPassword != request.NewPasswordRepeat) throw new DomainException(ErrorsKeys.PasswordsNotEqual);

        await _userFacade.ChangePassword(_userId, request.NewPassword, request.OldPassword);

        await UnitOfWork.SaveChanges();

        return new ChangeUserPasswordDto();
    }
}
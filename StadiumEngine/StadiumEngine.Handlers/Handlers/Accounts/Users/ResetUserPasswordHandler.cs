using AutoMapper;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Services.Facades.Accounts;
using StadiumEngine.Domain.Services.Infrastructure;
using StadiumEngine.DTO.Accounts.Users;
using StadiumEngine.Commands.Accounts.Users;

namespace StadiumEngine.Handlers.Handlers.Accounts.Users;

internal sealed class ResetUserPasswordHandler : BaseCommandHandler<ResetUserPasswordCommand, ResetUserPasswordDto>
{
    private readonly ISmsSender _smsSender;
    private readonly IUserCommandFacade _userFacade;

    public ResetUserPasswordHandler(
        IUserCommandFacade userFacade,
        ISmsSender smsSender,
        IMapper mapper,
        IUnitOfWork unitOfWork ) : base(
        mapper,
        null,
        unitOfWork,
        false )
    {
        _userFacade = userFacade;
        _smsSender = smsSender;
    }

    protected override async ValueTask<ResetUserPasswordDto> HandleCommand( ResetUserPasswordCommand request,
        CancellationToken cancellationToken )
    {
        ( User user, string password ) = await _userFacade.ResetPassword( request.PhoneNumber );
        await UnitOfWork.SaveChanges();

        await _smsSender.SendPassword( user.PhoneNumber, password, user.Language );

        return new ResetUserPasswordDto();
    }
}
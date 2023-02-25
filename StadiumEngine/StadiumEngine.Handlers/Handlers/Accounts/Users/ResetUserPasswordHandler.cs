using AutoMapper;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Services;
using StadiumEngine.Domain.Services.Facades.Accounts;
using StadiumEngine.Domain.Services.Infrastructure;
using StadiumEngine.DTO.Accounts.Users;
using StadiumEngine.Handlers.Commands.Accounts.Users;

namespace StadiumEngine.Handlers.Handlers.Accounts.Users;

internal sealed class ResetUserPasswordHandler : BaseRequestHandler<ResetUserPasswordCommand, ResetUserPasswordDto>
{
    private readonly IUserFacade _userFacade;
    private readonly ISmsSender _smsSender;

    public ResetUserPasswordHandler(
        IUserFacade userFacade,
        ISmsSender smsSender,
        IMapper mapper, 
        IUnitOfWork unitOfWork) : base(mapper, null, unitOfWork)
    {
        _userFacade = userFacade;
        _smsSender = smsSender;
    }

    public override async ValueTask<ResetUserPasswordDto> Handle(ResetUserPasswordCommand request, CancellationToken cancellationToken)
    {
        var (user, password) = await _userFacade.ResetPassword(request.PhoneNumber);
        await UnitOfWork.SaveChanges();
        
        await _smsSender.SendPassword(user.PhoneNumber, password, user.Language);

        return new ResetUserPasswordDto();
    }
}
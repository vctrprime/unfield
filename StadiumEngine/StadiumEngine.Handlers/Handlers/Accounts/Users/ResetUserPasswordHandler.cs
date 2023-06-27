using AutoMapper;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Services.Application.Accounts;
using StadiumEngine.Domain.Services.Infrastructure;
using StadiumEngine.DTO.Accounts.Users;
using StadiumEngine.Commands.Accounts.Users;

namespace StadiumEngine.Handlers.Handlers.Accounts.Users;

internal sealed class ResetUserPasswordHandler : BaseCommandHandler<ResetUserPasswordCommand, ResetUserPasswordDto>
{
    private readonly ISmsSender _smsSender;
    private readonly IUserCommandService _commandService;

    public ResetUserPasswordHandler(
        IUserCommandService commandService,
        ISmsSender smsSender,
        IMapper mapper,
        IUnitOfWork unitOfWork ) : base(
        mapper,
        null,
        unitOfWork,
        false )
    {
        _commandService = commandService;
        _smsSender = smsSender;
    }

    protected override async ValueTask<ResetUserPasswordDto> HandleCommandAsync( ResetUserPasswordCommand request,
        CancellationToken cancellationToken )
    {
        ( User user, string password ) = await _commandService.ResetPasswordAsync( request.PhoneNumber );
        await UnitOfWork.SaveChangesAsync();

        await _smsSender.SendPasswordAsync( user.PhoneNumber, password, user.Language );

        return new ResetUserPasswordDto();
    }
}
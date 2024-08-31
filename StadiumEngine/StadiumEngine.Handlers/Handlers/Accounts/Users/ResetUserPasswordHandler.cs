using AutoMapper;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Services.Core.Accounts;
using StadiumEngine.Domain.Services.Infrastructure;
using StadiumEngine.DTO.Accounts.Users;
using StadiumEngine.Commands.Accounts.Users;

namespace StadiumEngine.Handlers.Handlers.Accounts.Users;

internal sealed class ResetUserPasswordHandler : BaseCommandHandler<ResetUserPasswordCommand, ResetUserPasswordDto>
{
    private readonly IUserCommandService _commandService;

    public ResetUserPasswordHandler(
        IUserCommandService commandService,
        ISmsSender smsSender,
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
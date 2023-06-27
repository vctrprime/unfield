using AutoMapper;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Services.Application.Accounts;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Accounts.Users;
using StadiumEngine.Commands.Accounts.Users;

namespace StadiumEngine.Handlers.Handlers.Accounts.Users;

internal class ChangeUserLanguageHandler : BaseCommandHandler<ChangeUserLanguageCommand, ChangeUserLanguageDto>
{
    private readonly IUserCommandService _commandService;

    public ChangeUserLanguageHandler(
        IUserCommandService commandService,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService,
        IUnitOfWork unitOfWork ) : base( mapper, claimsIdentityService, unitOfWork )
    {
        _commandService = commandService;
    }


    protected override async ValueTask<ChangeUserLanguageDto> HandleCommandAsync( ChangeUserLanguageCommand request,
        CancellationToken cancellationToken )
    {
        await _commandService.ChangeLanguageAsync( _userId, request.Language );
        return new ChangeUserLanguageDto();
    }
}
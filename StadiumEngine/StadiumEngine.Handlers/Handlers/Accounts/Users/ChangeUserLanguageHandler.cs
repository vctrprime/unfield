using AutoMapper;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Services.Facades.Accounts;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Accounts.Users;
using StadiumEngine.Handlers.Commands.Accounts.Users;

namespace StadiumEngine.Handlers.Handlers.Accounts.Users;

internal class ChangeUserLanguageHandler : BaseCommandHandler<ChangeUserLanguageCommand, ChangeUserLanguageDto>
{
    private readonly IUserCommandFacade _userFacade;

    public ChangeUserLanguageHandler(
        IUserCommandFacade userFacade,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService,
        IUnitOfWork unitOfWork ) : base( mapper, claimsIdentityService, unitOfWork )
    {
        _userFacade = userFacade;
    }


    protected override async ValueTask<ChangeUserLanguageDto> HandleCommand( ChangeUserLanguageCommand request,
        CancellationToken cancellationToken )
    {
        await _userFacade.ChangeLanguage( _userId, request.Language );
        return new ChangeUserLanguageDto();
    }
}
using AutoMapper;
using Unfield.Domain;
using Unfield.Domain.Services.Core.Accounts;
using Unfield.Domain.Services.Identity;
using Unfield.DTO.Accounts.Users;
using Unfield.Commands.Accounts.Users;

namespace Unfield.Handlers.Handlers.Accounts.Users;

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
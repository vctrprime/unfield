using AutoMapper;
using Unfield.Commands.Accounts.Users;
using Unfield.Domain;
using Unfield.Domain.Services.Core.Accounts;
using Unfield.Domain.Services.Identity;
using Unfield.DTO.Accounts.Users;

namespace Unfield.Handlers.Handlers.Accounts.Users;

internal sealed class ToggleUserStadiumHandler : BaseCommandHandler<ToggleUserStadiumCommand, ToggleUserStadiumDto>
{
    private readonly IUserCommandService _commandService;

    public ToggleUserStadiumHandler(
        IUserCommandService commandService,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService,
        IUnitOfWork unitOfWork
    ) : base( mapper, claimsIdentityService, unitOfWork )
    {
        _commandService = commandService;
    }


    protected override async ValueTask<ToggleUserStadiumDto> HandleCommandAsync( ToggleUserStadiumCommand request,
        CancellationToken cancellationToken )
    {
        await _commandService.ToggleUserStadiumAsync(
            request.UserId,
            request.StadiumId,
            _stadiumGroupId,
            _userId );
        return new ToggleUserStadiumDto();
    }
}
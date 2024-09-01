using AutoMapper;
using StadiumEngine.Commands.Accounts.Users;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Services.Core.Accounts;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Accounts.Users;

namespace StadiumEngine.Handlers.Handlers.Accounts.Users;

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
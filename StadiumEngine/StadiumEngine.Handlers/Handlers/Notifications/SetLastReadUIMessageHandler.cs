using AutoMapper;
using StadiumEngine.Commands.Notifications;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Services.Core.Notifications;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Notifications;

namespace StadiumEngine.Handlers.Handlers.Notifications;

internal sealed class SetLastReadUIMessageHandler : BaseCommandHandler<SetLastReadUIMessageCommand, SetLastReadUIMessageDto>
{
    private readonly IUIMessageLastReadCommandService _commandService;

    public SetLastReadUIMessageHandler(
        IUIMessageLastReadCommandService commandService,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService,
        IUnitOfWork unitOfWork ) : base( mapper, claimsIdentityService, unitOfWork )
    {
        _commandService = commandService;
    }

    protected override async ValueTask<SetLastReadUIMessageDto> HandleCommandAsync(
        SetLastReadUIMessageCommand request,
        CancellationToken cancellationToken )
    {
        await _commandService.SaveAsync( _userId, _currentStadiumId, request.MessageId );
        
        return new SetLastReadUIMessageDto();
    }
}
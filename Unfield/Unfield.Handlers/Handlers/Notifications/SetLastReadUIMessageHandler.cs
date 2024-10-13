using AutoMapper;
using Unfield.Commands.Notifications;
using Unfield.Domain;
using Unfield.Domain.Services.Core.Notifications;
using Unfield.Domain.Services.Identity;
using Unfield.DTO.Notifications;

namespace Unfield.Handlers.Handlers.Notifications;

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
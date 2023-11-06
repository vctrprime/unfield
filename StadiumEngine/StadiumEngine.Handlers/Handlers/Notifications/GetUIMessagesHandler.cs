using AutoMapper;
using StadiumEngine.Domain.Entities.Notifications;
using StadiumEngine.Domain.Services.Core.Notifications;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Notifications;
using StadiumEngine.Queries.Notifications;

namespace StadiumEngine.Handlers.Handlers.Notifications;

internal sealed class GetUIMessagesHandler : BaseRequestHandler<GetUIMessagesQuery, List<UIMessageDto>>
{
    private readonly IUIMessageQueryService _messageQueryService;
    private readonly IUIMessageLastReadQueryService _messageLastReadQueryService;

    public GetUIMessagesHandler(
        IUIMessageQueryService messageQueryService,
        IUIMessageLastReadQueryService messageLastReadQueryService,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService ) : base( mapper, claimsIdentityService )
    {
        _messageQueryService = messageQueryService;
        _messageLastReadQueryService = messageLastReadQueryService;
    }

    public override async ValueTask<List<UIMessageDto>> Handle(
        GetUIMessagesQuery request,
        CancellationToken cancellationToken )
    {
        List<UIMessage> messages = await _messageQueryService.GetByStadiumIdAsync( _currentStadiumId );

        List<UIMessageDto> messagesDto = Mapper.Map<List<UIMessageDto>>( messages );

        if ( messagesDto.Any() )
        {
            UIMessageLastRead messageLastRead = await _messageLastReadQueryService.GetForUserAndStadiumAsync( _userId, _currentStadiumId );
            messagesDto.ForEach(
                m =>
                {
                    m.IsRead = messageLastRead.MessageId >= m.Id;
                } );
        }

        return messagesDto;
    }
}
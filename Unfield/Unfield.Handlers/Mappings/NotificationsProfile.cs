using AutoMapper;
using Unfield.Domain.Entities.Notifications;
using Unfield.DTO.Notifications;

namespace Unfield.Handlers.Mappings;

public class NotificationsProfile : Profile
{
    public NotificationsProfile()
    {
        CreateMap<UIMessage, UIMessageDto>();
        CreateMap<UIMessageText, UIMessageTextDto>();
    }
}
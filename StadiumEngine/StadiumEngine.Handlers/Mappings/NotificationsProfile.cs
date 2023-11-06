using AutoMapper;
using StadiumEngine.Domain.Entities.Notifications;
using StadiumEngine.DTO.Notifications;

namespace StadiumEngine.Handlers.Mappings;

public class NotificationsProfile : Profile
{
    public NotificationsProfile()
    {
        CreateMap<UIMessage, UIMessageDto>();
        CreateMap<UIMessageText, UIMessageTextDto>();
    }
}
using AutoMapper;
using Unfield.Commands.Schedule;
using Unfield.Common.Static;
using Unfield.Domain.Entities.Bookings;
using Unfield.Domain.Services.Models.Schedule;
using Unfield.DTO.Schedule;

namespace Unfield.Handlers.Mappings;

public class SchedulerProfile : Profile
{
    public SchedulerProfile()
    {
        CreateMap<BookingCustomer, BookingCustomerDto>();
        CreateMap<BookingInventory, BookingInventoryDto>();
        CreateMap<BookingCost, BookingCostDto>();
        CreateMap<BookingPromo, BookingPromoDto>();
        CreateMap<Booking, BookingDto>()
            .ForMember( dest => dest.LockerRoom, act => act.MapFrom( s => s.BookingLockerRoom!.LockerRoom ) );
        CreateMap<SchedulerEvent, SchedulerEventDto>();
        
        CreateMap<SaveSchedulerBookingDataCommandCost, BookingCost>();
        CreateMap<SaveSchedulerBookingDataCommandInventory, BookingInventory>();
        CreateMap<SaveSchedulerBookingDataCommandCustomer, BookingCustomer>();

        CreateMap<BookingListItem, BookingListItemDto>();
        CreateMap<SchedulerEventDto, BookingListItemDto>()
            .ForMember( dest => dest.OriginalData, act => act.MapFrom( s => s.Data ) )
            .ForMember( dest => dest.Day, act => act.MapFrom( s => s.Start.Date ) );
    }
}
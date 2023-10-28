using AutoMapper;
using StadiumEngine.Commands.Schedule;
using StadiumEngine.Common.Static;
using StadiumEngine.Domain.Entities.Bookings;
using StadiumEngine.Domain.Services.Models.Schedule;
using StadiumEngine.DTO.Schedule;

namespace StadiumEngine.Handlers.Mappings;

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
        CreateMap<BookingDto, BookingListItemDto>()
            .ForMember( dest => dest.Time, act => act.MapFrom(  s => TimePointParser.Parse( s.StartHour ) ) );
    }
}
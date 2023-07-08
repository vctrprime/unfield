using AutoMapper;
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
        CreateMap<Booking, BookingDto>();
        CreateMap<SchedulerEvent, SchedulerEventDto>();
    }
}
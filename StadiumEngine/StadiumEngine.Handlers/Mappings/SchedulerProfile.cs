using AutoMapper;
using StadiumEngine.Domain.Entities.Bookings;
using StadiumEngine.DTO.Schedule;

namespace StadiumEngine.Handlers.Mappings;

public class SchedulerProfile : Profile
{
    public SchedulerProfile()
    {
        CreateMap<Booking, BookingDto>();
    }
}
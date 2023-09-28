using Mediator;
using StadiumEngine.DTO.Schedule;

namespace StadiumEngine.Commands.Schedule;

public sealed class SaveSchedulerBookingDataCommand : BaseCommand, IRequest<SchedulerEventDto>
{
    public bool IsNew { get; set; }
    public bool AutoLockerRoom { get; set; }
    public string BookingNumber { get; set; } = null!;
    
    public decimal HoursCount { get; set; }

    public decimal? ManualDiscount { get; set; }

    public string Language { get; set; } = "ru";
    
    public bool IsWeekly { get; set; }
    public bool EditOneInRow { get; set; }
    public int? LockerRoomId { get; set; }
    public int TariffId { get; set; }
    public DateTime Day { get; set; }
    
    public SaveSchedulerBookingDataCommandCustomer Customer { get; set; } = null!;
    public List<SaveSchedulerBookingDataCommandCost> Costs { get; set; } = null!;
    public List<SaveSchedulerBookingDataCommandInventory> Inventories { get; set; } = new();
}

public sealed class SaveSchedulerBookingDataCommandCost
{
    public decimal StartHour { get; set; }
    
    public decimal EndHour { get; set; }
    
    public decimal Cost { get; set; }
}

public sealed class SaveSchedulerBookingDataCommandInventory
{
    public int InventoryId { get; set; }
    
    public decimal Price { get; set; }
    
    public decimal Quantity { get; set; }
    
    public decimal Amount { get; set; }
}

public sealed class SaveSchedulerBookingDataCommandCustomer
{
    public string Name { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;
}
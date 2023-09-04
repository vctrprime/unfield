using Mediator;
using StadiumEngine.DTO.Schedule;

namespace StadiumEngine.Commands.Schedule;

public sealed class SaveBookingDataCommand : BaseCommand, IRequest<SaveBookingDataDto>
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
    
    public SaveBookingCommandCustomer Customer { get; set; } = null!;
    public List<SaveBookingCommandCost> Costs { get; set; } = null!;
    public List<SaveBookingCommandInventory> Inventories { get; set; } = new();
}

public sealed class SaveBookingCommandCost
{
    public decimal StartHour { get; set; }
    
    public decimal EndHour { get; set; }
    
    public decimal Cost { get; set; }
}

public sealed class SaveBookingCommandInventory
{
    public int InventoryId { get; set; }
    
    public decimal Price { get; set; }
    
    public decimal Quantity { get; set; }
    
    public decimal Amount { get; set; }
}

public sealed class SaveBookingCommandCustomer
{
    public string Name { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;
}
using Mediator;
using StadiumEngine.DTO.BookingForm;

namespace StadiumEngine.Commands.BookingForm;

public sealed class FillBookingDataCommand : IRequest<FillBookingDataDto>
{
    public string BookingNumber { get; set; } = null!;
    
    public decimal HoursCount { get; set; }
    
    public decimal Amount { get; set; }
    
    public string? PromoCode { get; set; }
    
    public decimal? Discount { get; set; }

    public FillBookingDataCommandCustomer Customer { get; set; } = null!;
    public List<FillBookingDataCommandCost> Costs { get; set; } = null!;
    public List<FillBookingDataCommandInventory> Inventories { get; set; } = new();
}

public sealed class FillBookingDataCommandCost
{
    public decimal StartHour { get; set; }
    
    public decimal EndHour { get; set; }
    
    public decimal Cost { get; set; }
}

public sealed class FillBookingDataCommandInventory
{
    public int InventoryId { get; set; }
    
    public decimal Price { get; set; }
    
    public decimal Quantity { get; set; }
    
    public decimal Amount { get; set; }
}

public sealed class FillBookingDataCommandCustomer
{
    public string Name { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;
}
using Mediator;
using StadiumEngine.Common.Enums.Rates;
using StadiumEngine.DTO.BookingForm;

namespace StadiumEngine.Commands.BookingForm;

public sealed class FillBookingDataCommand : BaseCommand, IRequest<FillBookingDataDto>
{
    public string BookingNumber { get; set; } = null!;
    
    public decimal HoursCount { get; set; }
    
    public decimal Amount { get; set; }

    public decimal? PromoDiscount { get; set; }
    
    public decimal? ManualDiscount { get; set; }

    public string Language { get; set; } = "ru";

    public FillBookingDataCommandCustomer Customer { get; set; } = null!;
    public FillBookingDataCommandPromo? Promo { get; set; }
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

public sealed class FillBookingDataCommandPromo
{
    public string Code { get; set; } = null!;

    public PromoCodeType Type { get; set; }
    
    public decimal Value { get; set; }
}
using Unfield.Domain.Entities.Offers;

namespace Unfield.Domain.Entities.Bookings;

public class BookingInventory : BaseUserEntity
{
    public int BookingId { get; set; }
    public int InventoryId { get; set; }
    public decimal Price { get; set; }
    public decimal Quantity { get; set; }
    public decimal Amount { get; set; }
    
    public virtual Booking Booking { get; set; }
    public virtual Inventory Inventory { get; set; }
}
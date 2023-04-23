using System.ComponentModel.DataAnnotations.Schema;
using StadiumEngine.Domain.Entities.Offers;

namespace StadiumEngine.Domain.Entities.BookingForm;

[Table( "booking_inventory", Schema = "bookings" )]
public class BookingInventory : BaseBookingEntity
{
    [Column( "booking_id" )]
    public int BookingId { get; set; }
    
    [Column( "inventory_id" )]
    public int InventoryId { get; set; }
    
    [Column( "price" )]
    public decimal Price { get; set; }
    
    [Column( "quantity" )]
    public decimal Quantity { get; set; }
    
    [Column( "amount" )]
    public decimal Amount { get; set; }
    
    [ForeignKey( "BookingId" )]
    public virtual Booking Booking { get; set; }
    
    [ForeignKey( "InventoryId" )]
    public virtual Inventory Inventory { get; set; }
}
using System.Collections.Generic;
using StadiumEngine.Common.Enums.Offers;
using StadiumEngine.Domain.Entities.Bookings;

namespace StadiumEngine.Domain.Entities.Offers;

public class Inventory : BaseOfferEntity
{
    public Currency Currency { get; set; }
    public decimal Price { get; set; }
    public decimal Quantity { get; set; }
    
    public virtual ICollection<BookingInventory> BookingInventories { get; set; }
}
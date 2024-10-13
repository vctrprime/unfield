using System.Collections.Generic;
using Unfield.Common.Enums.Offers;
using Unfield.Domain.Entities.Bookings;

namespace Unfield.Domain.Entities.Offers;

public class Inventory : BaseOfferEntity
{
    public Currency Currency { get; set; }
    public decimal Price { get; set; }
    public decimal Quantity { get; set; }
    
    public virtual ICollection<BookingInventory> BookingInventories { get; set; }
}
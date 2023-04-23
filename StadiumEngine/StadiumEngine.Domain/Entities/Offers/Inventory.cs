using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using StadiumEngine.Common.Enums.Offers;
using StadiumEngine.Domain.Entities.BookingForm;

namespace StadiumEngine.Domain.Entities.Offers;

[Table( "inventory", Schema = "offers" )]
public class Inventory : BaseOfferEntity
{
    [Column( "currency" )]
    public Currency Currency { get; set; }

    [Column( "price" )]
    public decimal Price { get; set; }

    [Column( "quantity" )]
    public decimal Quantity { get; set; }
    
    public virtual ICollection<BookingInventory> Inventories { get; set; }
}
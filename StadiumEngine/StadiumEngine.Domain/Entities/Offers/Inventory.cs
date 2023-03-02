using System.ComponentModel.DataAnnotations.Schema;
using StadiumEngine.Common.Enums.Offers;

namespace StadiumEngine.Domain.Entities.Offers;

[Table("inventory", Schema = "offers")]
public class Inventory : BaseOfferEntity
{
    [Column("currency")]
    public Currency Currency { get; set; }
    
    [Column("price")]
    public decimal Price { get; set; }
    
    [Column("quantity")]
    public decimal Quantity { get; set; }
   
}
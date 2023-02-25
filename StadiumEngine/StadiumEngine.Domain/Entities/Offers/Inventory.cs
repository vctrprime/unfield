using System.ComponentModel.DataAnnotations.Schema;
using StadiumEngine.Common.Enums.Offers;

namespace StadiumEngine.Domain.Entities.Offers;

[Table("inventory", Schema = "offers")]
public class Inventory : BaseOffer
{
    [Column("currency")]
    public Currency Currency { get; set; }
    
    [Column("price")]
    public decimal Price { get; set; }
    
    [Column("quantity")]
    public decimal Quantity { get; set; }
    
    [Column("is_deleted")]
    public bool IsDeleted { get; set; }
    
    [Column("is_active")]
    public bool IsActive { get; set; }
}
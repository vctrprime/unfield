#nullable enable
using Unfield.Common.Enums.Offers;

namespace Unfield.Domain.Entities.Offers;

public class OffersSportKind : BaseUserEntity
{
    public SportKind SportKind { get; set; }
    public int? FieldId { get; set; }
    public int? InventoryId { get; set; }
    
    public virtual Field? Field { get; set; }
    public virtual Inventory? Inventory { get; set; }
    
}
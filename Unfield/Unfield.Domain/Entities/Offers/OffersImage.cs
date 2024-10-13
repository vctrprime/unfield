#nullable enable

namespace Unfield.Domain.Entities.Offers;

public class OffersImage : BaseUserEntity
{
    public string Path { get; set; } = null!;
    public int Order { get; set; }
    public int? FieldId { get; set; }
    public int? InventoryId { get; set; }

    public virtual Field? Field { get; set; }
    public virtual Inventory? Inventory { get; set; }
}
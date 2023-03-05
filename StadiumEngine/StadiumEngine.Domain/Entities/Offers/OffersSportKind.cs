#nullable enable
using System.ComponentModel.DataAnnotations.Schema;
using StadiumEngine.Common.Enums.Offers;

namespace StadiumEngine.Domain.Entities.Offers;

[Table( "sport_kind", Schema = "offers" )]
public class OffersSportKind : BaseRefEntity
{
    [Column( "field_id" )] public int? FieldId { get; set; }

    [ForeignKey( "FieldId" )] public virtual Field? Field { get; set; }

    [Column( "inventory_id" )] public int? InventoryId { get; set; }

    [ForeignKey( "InventoryId" )] public virtual Inventory? Inventory { get; set; }

    [Column( "sport_kind" )] public SportKind SportKind { get; set; }
}
#nullable enable
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Domain.Entities.Offers;

[Table( "image", Schema = "offers" )]
public class OffersImage : BaseUserEntity
{
    [Column( "path" )] public string Path { get; set; }

    [Column( "order_value" )] public int Order { get; set; }

    [Column( "field_id" )]
    [ForeignKey( "FieldId" )]
    public int? FieldId { get; set; }

    public virtual Field? Field { get; set; }

    [Column( "inventory_id" )]
    [ForeignKey( "InventoryId" )]
    public int? InventoryId { get; set; }

    public virtual Inventory? Inventory { get; set; }

    [NotMapped] public new string Name { get; set; }
    [NotMapped] public new string Description { get; set; }
}
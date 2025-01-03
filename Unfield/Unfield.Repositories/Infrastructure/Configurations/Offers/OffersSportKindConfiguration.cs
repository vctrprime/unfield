using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Unfield.Domain.Entities;
using Unfield.Domain.Entities.Offers;

namespace Unfield.Repositories.Infrastructure.Configurations.Offers;

internal class OffersSportKindConfiguration : BaseUserEntityConfiguration, IEntityTypeConfiguration<OffersSportKind>
{
    public void Configure( EntityTypeBuilder<OffersSportKind> builder )
    {
        builder.ToTable( "sport_kind", "offers" );
        BaseUserEntityConfigure( builder );
        
        builder.HasOne( x => x.UserCreated )
            .WithMany( x => x.CreatedOffersSportKinds )
            .HasForeignKey( x => x.UserCreatedId );
        
        builder.Property( p => p.SportKind ).HasColumnName( "sport_kind" );
        builder.Property( p => p.FieldId ).HasColumnName( "field_id" );
        builder.Property( p => p.InventoryId ).HasColumnName( "inventory_id" );
        
        builder.HasOne( x => x.Field )
            .WithMany( x => x.SportKinds )
            .HasForeignKey( x => x.FieldId );
        builder.HasOne( x => x.Inventory )
            .WithMany( x => x.SportKinds )
            .HasForeignKey( x => x.InventoryId );
    }
}
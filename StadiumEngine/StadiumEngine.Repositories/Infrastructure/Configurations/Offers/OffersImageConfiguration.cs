using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StadiumEngine.Domain.Entities;
using StadiumEngine.Domain.Entities.Offers;

namespace StadiumEngine.Repositories.Infrastructure.Configurations.Offers;

internal class OffersImageConfiguration : IEntityTypeConfiguration<OffersImage>
{
    public void Configure( EntityTypeBuilder<OffersImage> builder )
    {
        builder.ToTable( "image", "offers" );
        builder.HasBaseType( typeof( BaseUserEntity ) );
        
        builder.HasOne( x => x.UserCreated )
            .WithMany( x => x.CreatedOffersImages )
            .HasForeignKey( x => x.UserCreatedId );
        
        builder.Property( p => p.Path ).HasColumnName( "path" );
        builder.Property( p => p.Order ).HasColumnName( "order_value" );
        builder.Property( p => p.FieldId ).HasColumnName( "field_id" );
        builder.Property( p => p.InventoryId ).HasColumnName( "inventory_id" );
        
        builder.HasOne( x => x.Field )
            .WithMany( x => x.Images )
            .HasForeignKey( x => x.FieldId );
        builder.HasOne( x => x.Inventory )
            .WithMany( x => x.Images )
            .HasForeignKey( x => x.InventoryId );
    }
}
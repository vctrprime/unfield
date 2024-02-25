using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StadiumEngine.Domain.Entities;
using StadiumEngine.Domain.Entities.Offers;

namespace StadiumEngine.Repositories.Infrastructure.Configurations.Offers;

internal class InventoryConfiguration : IEntityTypeConfiguration<Inventory>
{
    public void Configure( EntityTypeBuilder<Inventory> builder )
    {
        builder.ToTable( "inventory", "offers" );
        builder.HasBaseType( typeof( BaseOfferEntity ) );
        
        builder.HasOne( x => x.UserCreated )
            .WithMany( x => x.CreatedInventories )
            .HasForeignKey( x => x.UserCreatedId );
        
        builder.HasOne( x => x.UserModified )
            .WithMany( x => x.LastModifiedInventories )
            .HasForeignKey( x => x.UserModifiedId );
        
        builder.HasOne( x => x.Stadium )
            .WithMany( x => x.Inventories )
            .HasForeignKey( x => x.StadiumId );

        builder.Property( p => p.Currency ).HasColumnName( "currency" );
        builder.Property( p => p.Price ).HasColumnName( "price" );
        builder.Property( p => p.Quantity ).HasColumnName( "quantity" );
    }
}
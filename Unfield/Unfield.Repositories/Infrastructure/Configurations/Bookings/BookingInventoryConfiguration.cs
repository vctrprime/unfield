using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Unfield.Domain.Entities;
using Unfield.Domain.Entities.Bookings;

namespace Unfield.Repositories.Infrastructure.Configurations.Bookings;

internal class BookingInventoryConfiguration : BaseUserEntityConfiguration, IEntityTypeConfiguration<BookingInventory>
{
    public void Configure( EntityTypeBuilder<BookingInventory> builder )
    {
        builder.ToTable( "booking_inventory", "bookings" );
        BaseUserEntityConfigure( builder );
        
        builder.HasOne( x => x.UserCreated )
            .WithMany( x => x.CreatedBookingInventories )
            .HasForeignKey( x => x.UserCreatedId );
        
        builder.HasOne( x => x.UserModified )
            .WithMany( x => x.LastModifiedBookingInventories )
            .HasForeignKey( x => x.UserModifiedId );
        
        builder.Property( p => p.BookingId ).HasColumnName( "booking_id" ).IsRequired();
        builder.Property( p => p.InventoryId ).HasColumnName( "inventory_id" );
        builder.Property( p => p.Price ).HasColumnName( "price" );
        builder.Property( p => p.Quantity ).HasColumnName( "quantity" );
        builder.Property( p => p.Amount ).HasColumnName( "amount" );
        
        builder.HasOne( x => x.Booking )
            .WithMany( x => x.Inventories )
            .HasForeignKey( x => x.BookingId );
        builder.HasOne( x => x.Inventory )
            .WithMany( x => x.BookingInventories )
            .HasForeignKey( x => x.InventoryId );
    }
}
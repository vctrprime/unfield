using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StadiumEngine.Domain.Entities;
using StadiumEngine.Domain.Entities.Bookings;

namespace StadiumEngine.Repositories.Infrastructure.Configurations.Bookings;

internal class BookingPromoConfiguration : IEntityTypeConfiguration<BookingPromo>
{
    public void Configure( EntityTypeBuilder<BookingPromo> builder )
    {
        builder.ToTable( "booking_promo", "bookings" );
        builder.HasBaseType( typeof( BaseUserEntity ) );
        
        builder.HasOne( x => x.UserCreated )
            .WithMany( x => x.CreatedBookingsPromos )
            .HasForeignKey( x => x.UserCreatedId );
        
        builder.HasOne( x => x.UserModified )
            .WithMany( x => x.LastModifiedBookingsPromos )
            .HasForeignKey( x => x.UserModifiedId );
        
        builder.Property( p => p.BookingId ).HasColumnName( "booking_id" ).IsRequired();
        builder.Property( p => p.Code ).HasColumnName( "code" );
        builder.Property( p => p.Type ).HasColumnName( "type" );
        builder.Property( p => p.Value ).HasColumnName( "value" );
        
        builder.HasOne( x => x.Booking )
            .WithOne( x => x.Promo )
            .HasForeignKey<BookingPromo>( x => x.BookingId );
    }
}
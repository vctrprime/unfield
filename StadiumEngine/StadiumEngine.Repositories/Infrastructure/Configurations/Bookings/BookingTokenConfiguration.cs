using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StadiumEngine.Domain.Entities;
using StadiumEngine.Domain.Entities.Bookings;

namespace StadiumEngine.Repositories.Infrastructure.Configurations.Bookings;

internal class BookingTokenConfiguration : BaseEntityConfiguration, IEntityTypeConfiguration<BookingToken>
{
    public void Configure( EntityTypeBuilder<BookingToken> builder )
    {
        builder.ToTable( "booking_token", "bookings" );
        BaseEntityConfigure( builder );
        
        builder.Property( p => p.BookingId ).HasColumnName( "booking_id" ).IsRequired();
        builder.Property( p => p.Token ).HasColumnName( "token" ).IsRequired();
        builder.Property( p => p.Type ).HasColumnName( "type" ).IsRequired();
        
        builder.HasOne( x => x.Booking )
            .WithMany( x => x.Tokens )
            .HasForeignKey( x => x.BookingId );
    }
}
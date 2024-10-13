using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Unfield.Domain.Entities;
using Unfield.Domain.Entities.Bookings;

namespace Unfield.Repositories.Infrastructure.Configurations.Bookings;

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
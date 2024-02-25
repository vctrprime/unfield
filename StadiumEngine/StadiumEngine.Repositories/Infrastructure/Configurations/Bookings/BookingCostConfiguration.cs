using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StadiumEngine.Domain.Entities;
using StadiumEngine.Domain.Entities.Bookings;

namespace StadiumEngine.Repositories.Infrastructure.Configurations.Bookings;

internal class BookingCostConfiguration : BaseUserEntityConfiguration, IEntityTypeConfiguration<BookingCost>
{
    public void Configure( EntityTypeBuilder<BookingCost> builder )
    {
        builder.ToTable( "booking_cost", "bookings" );
        BaseUserEntityConfigure( builder );
        
        builder.HasOne( x => x.UserCreated )
            .WithMany( x => x.CreatedBookingsCosts )
            .HasForeignKey( x => x.UserCreatedId );
        
        builder.HasOne( x => x.UserModified )
            .WithMany( x => x.LastModifiedBookingCosts )
            .HasForeignKey( x => x.UserModifiedId );
        
        builder.Property( p => p.BookingId ).HasColumnName( "booking_id" ).IsRequired();
        builder.Property( p => p.StartHour ).HasColumnName( "start_hour" );
        builder.Property( p => p.EndHour ).HasColumnName( "end_hour" );
        builder.Property( p => p.Cost ).HasColumnName( "cost" );
        
        builder.HasOne( x => x.Booking )
            .WithMany( x => x.Costs )
            .HasForeignKey( x => x.BookingId );
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StadiumEngine.Domain.Entities;
using StadiumEngine.Domain.Entities.Bookings;

namespace StadiumEngine.Repositories.Infrastructure.Configurations.Bookings;

internal class BookingWeeklyExcludeDayConfiguration : IEntityTypeConfiguration<BookingWeeklyExcludeDay>
{
    public void Configure( EntityTypeBuilder<BookingWeeklyExcludeDay> builder )
    {
        builder.ToTable( "booking_weekly_exclude_day", "bookings" );
        builder.HasBaseType( typeof( BaseUserEntity ) );
        
        builder.HasOne( x => x.UserCreated )
            .WithMany( x => x.CreatedBookingsWeeklyExcludeDays )
            .HasForeignKey( x => x.UserCreatedId );
        
        builder.HasOne( x => x.UserModified )
            .WithMany( x => x.LastModifiedBookingsWeeklyExcludeDays )
            .HasForeignKey( x => x.UserModifiedId );
        
        builder.Property( p => p.BookingId ).HasColumnName( "booking_id" ).IsRequired();
        builder.Property( p => p.Day ).HasColumnName( "day" ).HasColumnType( "timestamp without time zone" );
        builder.Property( p => p.Reason ).HasColumnName( "reason" );
        
        builder.HasOne( x => x.Booking )
            .WithMany( x => x.WeeklyExcludeDays )
            .HasForeignKey( x => x.BookingId );
    }
}
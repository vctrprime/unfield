using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StadiumEngine.Domain.Entities;
using StadiumEngine.Domain.Entities.Bookings;

namespace StadiumEngine.Repositories.Infrastructure.Configurations.Bookings;

internal class BookingLockerRoomConfiguration : BaseUserEntityConfiguration, IEntityTypeConfiguration<BookingLockerRoom>
{
    public void Configure( EntityTypeBuilder<BookingLockerRoom> builder )
    {
        builder.ToTable( "booking_locker_room", "bookings" );
        BaseUserEntityConfigure( builder );
        
        builder.HasOne( x => x.UserCreated )
            .WithMany( x => x.CreatedBookingLockerRooms )
            .HasForeignKey( x => x.UserCreatedId );
        
        builder.HasOne( x => x.UserModified )
            .WithMany( x => x.LastModifiedBookingLockerRooms )
            .HasForeignKey( x => x.UserModifiedId );
        
        builder.Property( p => p.BookingId ).HasColumnName( "booking_id" ).IsRequired();
        builder.Property( p => p.LockerRoomId ).HasColumnName( "locker_room_id" );
        builder.Property( p => p.Start ).HasColumnName( "start" ).HasColumnType( "timestamp without time zone" );
        builder.Property( p => p.End ).HasColumnName( "end" ).HasColumnType( "timestamp without time zone" );
        
        builder.HasOne( x => x.LockerRoom )
            .WithMany( x => x.BookingLockerRooms )
            .HasForeignKey( x => x.LockerRoomId );
        builder.HasOne( x => x.Booking )
            .WithOne( x => x.BookingLockerRoom )
            .HasForeignKey<BookingLockerRoom>( x => x.BookingId );
    }
}
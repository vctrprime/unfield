using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Unfield.Domain.Entities;
using Unfield.Domain.Entities.Bookings;

namespace Unfield.Repositories.Infrastructure.Configurations.Bookings;

internal class BookingCustomerConfiguration : BaseUserEntityConfiguration, IEntityTypeConfiguration<BookingCustomer>
{
    public void Configure( EntityTypeBuilder<BookingCustomer> builder )
    {
        builder.ToTable( "booking_customer", "bookings" );
        BaseUserEntityConfigure( builder );
        
        builder.HasOne( x => x.UserCreated )
            .WithMany( x => x.CreatedBookingsCustomers )
            .HasForeignKey( x => x.UserCreatedId );
        
        builder.HasOne( x => x.UserModified )
            .WithMany( x => x.LastModifiedBookingsCustomers )
            .HasForeignKey( x => x.UserModifiedId );
        
        builder.Property( p => p.BookingId ).HasColumnName( "booking_id" ).IsRequired();
        builder.Property( p => p.Name ).HasColumnName( "name" );
        builder.Property( p => p.PhoneNumber ).HasColumnName( "phone_number" );
        
        builder.HasOne( x => x.Booking )
            .WithOne( x => x.Customer )
            .HasForeignKey<BookingCustomer>( x => x.BookingId );
    }
}
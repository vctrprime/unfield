using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StadiumEngine.Domain.Entities;
using StadiumEngine.Domain.Entities.Bookings;

namespace StadiumEngine.Repositories.Infrastructure.Configurations.Bookings;

internal class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure( EntityTypeBuilder<Booking> builder )
    {
        builder.ToTable( "booking", "bookings" );
        builder.HasBaseType( typeof( BaseUserEntity ) );
        
        builder.HasOne( x => x.UserCreated )
            .WithMany( x => x.CreatedBookings )
            .HasForeignKey( x => x.UserCreatedId );
        
        builder.HasOne( x => x.UserModified )
            .WithMany( x => x.LastModifiedBookings )
            .HasForeignKey( x => x.UserModifiedId );
        
        builder.Property( p => p.Number ).HasColumnName( "booking_number" ).IsRequired();
        builder.Property( p => p.Source ).HasColumnName( "source" );
        builder.Property( p => p.Day ).HasColumnName( "day" ).HasColumnType( "timestamp without time zone" );
        builder.Property( p => p.InventoryAmount ).HasColumnName( "inventory_amount" );
        builder.Property( p => p.FieldAmount ).HasColumnName( "field_amount" );
        builder.Property( p => p.TotalAmountBeforeDiscount ).HasColumnName( "total_amount_before_discount" );
        builder.Property( p => p.TotalAmountAfterDiscount ).HasColumnName( "total_amount_after_discount" );
        builder.Property( p => p.HoursCount ).HasColumnName( "hours_count" );
        builder.Property( p => p.FieldId ).HasColumnName( "field_id" );
        builder.Property( p => p.TariffId ).HasColumnName( "tariff_id" );
        builder.Property( p => p.IsDraft ).HasColumnName( "is_draft" );
        builder.Property( p => p.IsConfirmed ).HasColumnName( "is_confirmed" );
        builder.Property( p => p.IsCanceled ).HasColumnName( "is_canceled" );
        builder.Property( p => p.AccessCode ).HasColumnName( "access_code" ).IsRequired();
        builder.Property( p => p.PromoDiscount ).HasColumnName( "promo_discount" );
        builder.Property( p => p.ManualDiscount ).HasColumnName( "manual_discount" );
        builder.Property( p => p.Note ).HasColumnName( "note" );
        builder.Property( p => p.IsWeekly ).HasColumnName( "is_weekly" );
        builder.Property( p => p.IsWeeklyStoppedDate ).HasColumnName( "is_weekly_stopped_date" );
        builder.Property( p => p.IsLastVersion ).HasColumnName( "is_last_version" );
        builder.Property( p => p.CancelReason ).HasColumnName( "cancel_reason" );
        
        builder.HasOne( x => x.Field )
            .WithMany( x => x.Bookings )
            .HasForeignKey( x => x.FieldId );
        builder.HasOne( x => x.Tariff )
            .WithMany( x => x.Bookings )
            .HasForeignKey( x => x.TariffId );
    }
}
#nullable enable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using StadiumEngine.Common.Enums.BookingForm;
using StadiumEngine.Domain.Entities.Offers;
using StadiumEngine.Domain.Entities.Rates;

namespace StadiumEngine.Domain.Entities.BookingForm;

[Table( "booking", Schema = "bookings" )]
public class Booking : BaseBookingEntity
{
    [Column( "booking_number" )]
    public string Number { get; set; } = null!;
    
    [Column( "source" )]
    public BookingSource Source { get; set; }
    
    [Column( "day" )]
    public DateTime Day { get; set; }
    
    [Column( "amount" )]
    public decimal Amount { get; set; }
    
    [Column( "start_hour" )]
    public decimal StartHour { get; set; }
    
    [Column( "hours_count" )]
    public decimal HoursCount { get; set; }
    
    [Column( "field_id" )]
    public int FieldId { get; set; }
    
    [Column( "tariff_id" )]
    public int TariffId { get; set; }
    
    [Column( "locker_room_id" )]
    public int? LockerRoomId { get; set; }
    
    [Column( "is_draft" )]
    public bool IsDraft { get; set; }
    
    [Column( "is_confirmed" )]
    public bool IsConfirmed { get; set; }
    
    [Column( "is_canceled" )]
    public bool IsCanceled { get; set; }
    
    [Column( "access_code" )]
    public string AccessCode { get; set; } = null!;
    
    [Column( "promo_code" )]
    public string? PromoCode { get; set; }
    
    [Column( "note" )]
    public string? Note { get; set; }
    
    [ForeignKey( "FieldId" )]
    public virtual Field Field { get; set; } = null!;

    [ForeignKey( "TariffId" )]
    public virtual Tariff Tariff { get; set; } = null!;

    [ForeignKey( "LockerRoomId" )]
    public virtual LockerRoom? LockerRoom { get; set; }
    
    public virtual BookingCustomer Customer { get; set; } = null!;
    public virtual ICollection<BookingCost> Costs { get; set; } = null!;
    public virtual ICollection<BookingInventory> Inventories { get; set; } = null!;
}
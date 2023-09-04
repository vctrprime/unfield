#nullable enable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using StadiumEngine.Common.Enums.BookingForm;
using StadiumEngine.Domain.Entities.Offers;
using StadiumEngine.Domain.Entities.Rates;

namespace StadiumEngine.Domain.Entities.Bookings;

[Table( "booking", Schema = "bookings" )]
public class Booking : BaseBookingEntity
{
    [Column( "booking_number" )]
    public string Number { get; set; } = null!;
    
    [Column( "source" )]
    public BookingSource Source { get; set; }
    
    [Column( "day", TypeName = "timestamp without time zone")]
    public DateTime Day { get; set; }
    
    [Column( "inventory_amount" )]
    public decimal InventoryAmount { get; set; }
    
    [Column( "field_amount" )]
    public decimal FieldAmount { get; set; }
    
    [Column( "total_amount_before_discount" )] //InventoryAmount + FieldAmount
    public decimal TotalAmountBeforeDiscount { get; set; }
    
    [Column( "total_amount_after_discount" )] //TotalAmountBeforeDiscount - PromoDiscount - ManualDiscount
    public decimal TotalAmountAfterDiscount { get; set; }
    
    [Column( "start_hour" )]
    public decimal StartHour { get; set; }
    
    [Column( "hours_count" )]
    public decimal HoursCount { get; set; }
    
    [Column( "field_id" )]
    public int FieldId { get; set; }
    
    [Column( "tariff_id" )]
    public int TariffId { get; set; }
    
    [Column( "is_draft" )]
    public bool IsDraft { get; set; }
    
    [Column( "is_confirmed" )]
    public bool IsConfirmed { get; set; }
    
    [Column( "is_canceled" )]
    public bool IsCanceled { get; set; }
    
    [Column( "access_code" )]
    public string AccessCode { get; set; } = null!;
    
    [Column( "promo_discount" )]
    public decimal? PromoDiscount { get; set; }
    
    [Column( "manual_discount" )]
    public decimal? ManualDiscount { get; set; }
    
    [Column( "note" )]
    public string? Note { get; set; }

    [Column( "is_weekly" )]
    public bool IsWeekly { get; set; } = false;
    
    [Column( "is_weekly_stopped_date" )]
    public DateTime? IsWeeklyStoppedDate { get; set; }
    
    [Column("is_last_version")]
    public bool IsLastVersion { get; set; }
    
    [Column("close_version_date")]
    public DateTime? CloseVersionDate { get; set; }
    
    [ForeignKey( "FieldId" )]
    public virtual Field Field { get; set; } = null!;

    [ForeignKey( "TariffId" )]
    public virtual Tariff Tariff { get; set; } = null!;

    public virtual BookingLockerRoom? BookingLockerRoom { get; set; }
    public virtual BookingCustomer Customer { get; set; } = null!;
    public virtual BookingPromo? Promo { get; set; }
    public virtual ICollection<BookingCost> Costs { get; set; } = null!;
    public virtual ICollection<BookingInventory> Inventories { get; set; } = null!;
    public virtual ICollection<BookingWeeklyExcludeDay> WeeklyExcludeDays { get; set; } = null!;
}
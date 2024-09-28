#nullable enable
using System;
using System.Collections.Generic;
using StadiumEngine.Common.Enums.Bookings;
using StadiumEngine.Domain.Entities.Offers;
using StadiumEngine.Domain.Entities.Rates;

namespace StadiumEngine.Domain.Entities.Bookings;

public class Booking : BaseUserEntity
{
    public string Number { get; set; } = null!;
    public BookingSource Source { get; set; }
    public DateTime Day { get; set; }
    public decimal InventoryAmount { get; set; }
    public decimal FieldAmount { get; set; }
    
    //InventoryAmount + FieldAmount
    public decimal TotalAmountBeforeDiscount { get; set; }
    
    //TotalAmountBeforeDiscount - PromoDiscount - ManualDiscount
    public decimal TotalAmountAfterDiscount { get; set; }
    public decimal StartHour { get; set; }
    public decimal HoursCount { get; set; }
    public int FieldId { get; set; }
    public int TariffId { get; set; }
    public bool IsDraft { get; set; }
    public bool IsConfirmed { get; set; }
    public bool IsCanceled { get; set; }
    public string AccessCode { get; set; } = null!;
    public decimal? PromoDiscount { get; set; }
    public decimal? ManualDiscount { get; set; }
    public string? Note { get; set; }
    public bool IsWeekly { get; set; }
    public DateTime? IsWeeklyStoppedDate { get; set; }
    public bool IsLastVersion { get; set; }
    public DateTime? CloseVersionDate { get; set; }
    public string? CancelReason { get; set; }
    public bool? CancelByCustomer { get; set; }
    public virtual Field Field { get; set; } = null!;
    public virtual Tariff Tariff { get; set; } = null!;

    public virtual BookingLockerRoom? BookingLockerRoom { get; set; }
    public virtual BookingCustomer Customer { get; set; } = null!;
    public virtual BookingPromo? Promo { get; set; }
    public virtual ICollection<BookingCost> Costs { get; set; } = null!;
    public virtual ICollection<BookingInventory> Inventories { get; set; } = null!;
    public virtual ICollection<BookingWeeklyExcludeDay> WeeklyExcludeDays { get; set; } = null!;
    public virtual ICollection<BookingToken> Tokens { get; set; } = null!;
    
}
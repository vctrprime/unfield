using System;
using System.Collections.Generic;
using StadiumEngine.Domain.Entities.Bookings;
using StadiumEngine.Domain.Entities.Notifications;
using StadiumEngine.Domain.Entities.Offers;
using StadiumEngine.Domain.Entities.Rates;
using StadiumEngine.Domain.Entities.Settings;

namespace StadiumEngine.Domain.Entities.Accounts;

public class User : BaseUserEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public int LegalId { get; set; }
    public virtual Legal Legal { get; set; }
    public int? RoleId { get; set; }
    public virtual Role Role { get; set; }
    public bool IsSuperuser { get; set; }
    public DateTime? LastLoginDate { get; set; }
    public bool IsDeleted { get; set; }
    public string Language { get; set; }
    public bool IsAdmin { get; set; }
    
    public virtual ICollection<UserStadium> UserStadiums { get; set; }
    public virtual ICollection<UIMessageLastRead> UIMessageLastReads { get; set; }
    
    public virtual ICollection<User> CreatedUsers { get; set; }
    public virtual ICollection<User> LastModifiedUsers { get; set; }
    public virtual ICollection<Role> CreatedRoles { get; set; }
    public virtual ICollection<Role> LastModifiedRoles { get; set; }
    public virtual ICollection<RolePermission> CreatedRolePermissions { get; set; }
    public virtual ICollection<RolePermission> LastModifiedRolePermissions { get; set; }
    public virtual ICollection<UserStadium> CreatedUserStadiums { get; set; }
    public virtual ICollection<UserStadium> LastModifiedUserStadiums { get; set; }
    public virtual ICollection<LockerRoom> CreatedLockerRooms { get; set; }
    public virtual ICollection<LockerRoom> LastModifiedLockerRooms { get; set; }
    public virtual ICollection<Field> CreatedFields { get; set; }
    public virtual ICollection<Field> LastModifiedFields { get; set; }
    public virtual ICollection<Inventory> CreatedInventories { get; set; }
    public virtual ICollection<Inventory> LastModifiedInventories { get; set; }
    public virtual ICollection<OffersImage> CreatedOffersImages { get; set; }
    public virtual ICollection<OffersSportKind> CreatedOffersSportKinds { get; set; }
    public virtual ICollection<Tariff> CreatedTariffs { get; set; }
    public virtual ICollection<Tariff> LastModifiedTariffs { get; set; }
    public virtual ICollection<TariffDayInterval> CreatedTariffDayIntervals { get; set; }
    public virtual ICollection<TariffDayInterval> LastModifiedTariffDayIntervals { get; set; }
    public virtual ICollection<Price> CreatedPrices { get; set; }
    public virtual ICollection<Price> LastModifiedPrices { get; set; }
    public virtual ICollection<PriceGroup> CreatedPriceGroups { get; set; }
    public virtual ICollection<PriceGroup> LastModifiedPriceGroups { get; set; }
    public virtual ICollection<MainSettings> LastModifiedMainSettings { get; set; }
    public virtual ICollection<PromoCode> CreatedPromoCodes { get; set; }
    public virtual ICollection<PromoCode> LastModifiedPromoCodes { get; set; }
    public virtual ICollection<Booking> CreatedBookings { get; set; }
    public virtual ICollection<Booking> LastModifiedBookings { get; set; }
    public virtual ICollection<BookingCustomer> CreatedBookingsCustomers { get; set; }
    public virtual ICollection<BookingCustomer> LastModifiedBookingsCustomers { get; set; }
    public virtual ICollection<BookingPromo> CreatedBookingsPromos { get; set; }
    public virtual ICollection<BookingPromo> LastModifiedBookingsPromos { get; set; }
    public virtual ICollection<BookingCost> CreatedBookingsCosts { get; set; }
    public virtual ICollection<BookingCost> LastModifiedBookingCosts { get; set; }
    public virtual ICollection<BookingInventory> CreatedBookingInventories { get; set; }
    public virtual ICollection<BookingInventory> LastModifiedBookingInventories { get; set; }
    public virtual ICollection<Break> CreatedBreaks { get; set; }
    public virtual ICollection<Break> LastModifiedBreaks { get; set; }
    public virtual ICollection<BreakField> CreatedBreakFields { get; set; }
    public virtual ICollection<BreakField> LastModifiedBreakFields { get; set; }
    public virtual ICollection<BookingLockerRoom> CreatedBookingLockerRooms { get; set; }
    public virtual ICollection<BookingLockerRoom> LastModifiedBookingLockerRooms { get; set; }
    public virtual ICollection<BookingWeeklyExcludeDay> CreatedBookingsWeeklyExcludeDays { get; set; }
    public virtual ICollection<BookingWeeklyExcludeDay> LastModifiedBookingsWeeklyExcludeDays { get; set; }
}
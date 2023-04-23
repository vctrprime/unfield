using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using StadiumEngine.Domain.Entities.BookingForm;
using StadiumEngine.Domain.Entities.Offers;
using StadiumEngine.Domain.Entities.Rates;
using StadiumEngine.Domain.Entities.Settings;

namespace StadiumEngine.Domain.Entities.Accounts;

[Table( "user", Schema = "accounts" )]
[Index( nameof( PhoneNumber ), IsUnique = true )]
public class User : BaseUserEntity
{
    [Column( "last_name" )]
    public string LastName { get; set; }

    [Column( "phone_number" )]
    public string PhoneNumber { get; set; }

    [Column( "password" )]
    public string Password { get; set; }

    [Column( "legal_id" )]
    public int LegalId { get; set; }

    [ForeignKey( "LegalId" )]
    public virtual Legal Legal { get; set; }

    [Column( "role_id" )]
    public int? RoleId { get; set; }

    [ForeignKey( "RoleId" )]
    public virtual Role Role { get; set; }

    [Column( "is_superuser" )]
    public bool IsSuperuser { get; set; }

    [Column( "last_login_date" )]
    public DateTime? LastLoginDate { get; set; }

    [Column( "is_deleted" )]
    [DefaultValue( false )]
    public bool IsDeleted { get; set; }

    [Column( "language" )]
    public string Language { get; set; }

    [Column( "is_admin" )]
    [DefaultValue( false )]
    public bool IsAdmin { get; set; }

    [InverseProperty( "UserCreated" )]
    public virtual ICollection<User> CreatedUsers { get; set; }

    [InverseProperty( "UserModified" )]
    public virtual ICollection<User> LastModifiedUsers { get; set; }

    [InverseProperty( "UserCreated" )]
    public virtual ICollection<Role> CreatedRoles { get; set; }

    [InverseProperty( "UserModified" )]
    public virtual ICollection<Role> LastModifiedRoles { get; set; }

    [InverseProperty( "UserCreated" )]
    public virtual ICollection<RolePermission> CreatedRolePermissions { get; set; }

    [InverseProperty( "UserModified" )]
    public virtual ICollection<RolePermission> LastModifiedRolePermissions { get; set; }

    [InverseProperty( "UserCreated" )]
    public virtual ICollection<RoleStadium> CreatedRoleStadiums { get; set; }

    [InverseProperty( "UserModified" )]
    public virtual ICollection<RoleStadium> LastModifiedRoleStadiums { get; set; }

    [InverseProperty( "UserCreated" )]
    public virtual ICollection<LockerRoom> CreatedLockerRooms { get; set; }

    [InverseProperty( "UserModified" )]
    public virtual ICollection<LockerRoom> LastModifiedLockerRooms { get; set; }

    [InverseProperty( "UserCreated" )]
    public virtual ICollection<Field> CreatedFields { get; set; }

    [InverseProperty( "UserModified" )]
    public virtual ICollection<Field> LastModifiedFields { get; set; }

    [InverseProperty( "UserCreated" )]
    public virtual ICollection<Inventory> CreatedInventories { get; set; }

    [InverseProperty( "UserModified" )]
    public virtual ICollection<Inventory> LastModifiedInventories { get; set; }

    [InverseProperty( "UserCreated" )]
    public virtual ICollection<OffersImage> CreatedOffersImages { get; set; }

    [InverseProperty( "UserCreated" )]
    public virtual ICollection<OffersSportKind> CreatedOffersSportKinds { get; set; }

    [InverseProperty( "UserCreated" )]
    public virtual ICollection<Tariff> CreatedTariff { get; set; }

    [InverseProperty( "UserModified" )]
    public virtual ICollection<Tariff> LastModifiedTariff { get; set; }

    [InverseProperty( "UserCreated" )]
    public virtual ICollection<TariffDayInterval> CreatedTariffDayIntervals { get; set; }

    [InverseProperty( "UserModified" )]
    public virtual ICollection<TariffDayInterval> LastModifiedTariffDayIntervals { get; set; }

    [InverseProperty( "UserCreated" )]
    public virtual ICollection<Price> CreatedPrices { get; set; }

    [InverseProperty( "UserModified" )]
    public virtual ICollection<Price> LastModifiedPrices { get; set; }

    [InverseProperty( "UserCreated" )]
    public virtual ICollection<PriceGroup> CreatedPriceGroups { get; set; }

    [InverseProperty( "UserModified" )]
    public virtual ICollection<PriceGroup> LastModifiedPriceGroups { get; set; }
    
    [InverseProperty( "UserModified" )]
    public virtual ICollection<StadiumMainSettings> LastModifiedStadiumMainSettings { get; set; }
    
    [InverseProperty( "UserCreated" )]
    public virtual ICollection<PromoCode> CreatedPromoCodes { get; set; }

    [InverseProperty( "UserModified" )]
    public virtual ICollection<PromoCode> LastModifiedPromoCodes { get; set; }
    
    [InverseProperty( "UserCreated" )]
    public virtual ICollection<Booking> CreatedBookings { get; set; }

    [InverseProperty( "UserModified" )]
    public virtual ICollection<Booking> LastModifiedBookings { get; set; }
    
    [InverseProperty( "UserCreated" )]
    public virtual ICollection<BookingCustomer> CreatedBookingsCustomers { get; set; }

    [InverseProperty( "UserModified" )]
    public virtual ICollection<BookingCustomer> LastModifiedBookingsCustomers { get; set; }
    
    [InverseProperty( "UserCreated" )]
    public virtual ICollection<BookingCost> CreatedBookingsCosts { get; set; }

    [InverseProperty( "UserModified" )]
    public virtual ICollection<BookingCost> LastModifiedBookingCosts { get; set; }
    
    [InverseProperty( "UserCreated" )]
    public virtual ICollection<BookingInventory> CreatedBookingsInventories { get; set; }

    [InverseProperty( "UserModified" )]
    public virtual ICollection<BookingInventory> LastModifiedBookingsInventories { get; set; }
}
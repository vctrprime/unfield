using System.Collections.Generic;
using Unfield.Domain.Entities.Geo;
using Unfield.Domain.Entities.Notifications;
using Unfield.Domain.Entities.Offers;
using Unfield.Domain.Entities.Rates;
using Unfield.Domain.Entities.Settings;

namespace Unfield.Domain.Entities.Accounts;

public class Stadium : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int CityId { get; set; }
    public virtual City City { get; set; }
    public int StadiumGroupId { get; set; }
    public bool IsDeleted { get; set; }
    public virtual StadiumGroup StadiumGroup { get; set; }
    public string Address { get; set; }
    public string Token { get; set; }
    public string BookingFormHost { get; set; }

    public virtual ICollection<UserStadium> UserStadiums { get; set; }
    public virtual ICollection<LockerRoom> LockerRooms { get; set; }
    public virtual ICollection<Field> Fields { get; set; }
    public virtual ICollection<Inventory> Inventories { get; set; }
    public virtual ICollection<Tariff> Tariffs { get; set; }
    public virtual ICollection<PriceGroup> PriceGroups { get; set; }
    public virtual ICollection<Break> Breaks { get; set; }
    public virtual ICollection<UIMessage> UIMessages { get; set; }
    public virtual ICollection<UIMessageLastRead> UIMessageLastReads { get; set; }
    public virtual MainSettings MainSettings { get; set; }
}
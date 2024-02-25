using System.Collections.Generic;
using StadiumEngine.Domain.Entities.Geo;
using StadiumEngine.Domain.Entities.Notifications;
using StadiumEngine.Domain.Entities.Offers;
using StadiumEngine.Domain.Entities.Rates;
using StadiumEngine.Domain.Entities.Settings;

namespace StadiumEngine.Domain.Entities.Accounts;

public class Stadium : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int CityId { get; set; }
    public virtual City City { get; set; }
    public int LegalId { get; set; }
    public bool IsDeleted { get; set; }
    public virtual Legal Legal { get; set; }
    public string Address { get; set; }
    public string Token { get; set; }

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
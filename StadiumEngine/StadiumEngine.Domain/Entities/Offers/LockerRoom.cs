using System.Collections.Generic;
using StadiumEngine.Common.Enums.Offers;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Entities.Bookings;

namespace StadiumEngine.Domain.Entities.Offers;

public class LockerRoom : BaseUserEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public LockerRoomGender Gender { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
    public int StadiumId { get; set; }
    public virtual Stadium Stadium { get; set; }
    public virtual ICollection<BookingLockerRoom> BookingLockerRooms { get; set; }
}
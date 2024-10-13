using System.Collections.Generic;
using Unfield.Common.Enums.Offers;
using Unfield.Domain.Entities.Accounts;
using Unfield.Domain.Entities.Bookings;

namespace Unfield.Domain.Entities.Offers;

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
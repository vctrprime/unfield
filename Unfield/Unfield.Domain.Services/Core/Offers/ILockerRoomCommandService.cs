#nullable enable
using Unfield.Common.Enums.Offers;
using Unfield.Domain.Entities.Offers;

namespace Unfield.Domain.Services.Core.Offers;

public interface ILockerRoomCommandService
{
    void AddLockerRoom( LockerRoom lockerRoom );
    void UpdateLockerRoom( LockerRoom lockerRoom );
    Task DeleteLockerRoomAsync( int lockerRoomId, int stadiumId );
    Task<LockerRoomStatus> SyncStatusAsync( int lockerRoomId, int stadiumId, DateTime clientDate );
}
#nullable enable
using Unfield.Domain.Entities.Offers;

namespace Unfield.Domain.Repositories.Offers;

public interface ILockerRoomRepository
{
    Task<List<LockerRoom>> GetAllAsync( int stadiumId );
    Task<LockerRoom?> GetAsync( int lockerRoomId, int stadiumId );
    void Add( LockerRoom lockerRoom );
    void Update( LockerRoom lockerRoom );
    void Remove( LockerRoom lockerRoom );
}
#nullable enable
using StadiumEngine.Domain.Entities.Offers;

namespace StadiumEngine.Domain.Repositories.Offers;

public interface ILockerRoomRepository
{
    Task<List<LockerRoom>> GetAllAsync( int stadiumId );
    Task<LockerRoom?> GetAsync( int lockerRoomId, int stadiumId );
    void Add( LockerRoom lockerRoom );
    void Update( LockerRoom lockerRoom );
    void Remove( LockerRoom lockerRoom );
}
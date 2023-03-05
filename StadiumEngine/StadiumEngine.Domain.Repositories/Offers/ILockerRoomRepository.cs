#nullable enable
using System.Collections.Generic;
using System.Threading.Tasks;
using StadiumEngine.Domain.Entities.Offers;

namespace StadiumEngine.Domain.Repositories.Offers;

public interface ILockerRoomRepository
{
    Task<List<LockerRoom>> GetAll( int stadiumId );
    Task<LockerRoom?> Get( int lockerRoomId, int stadiumId );
    void Add( LockerRoom lockerRoom );
    void Update( LockerRoom lockerRoom );
    void Remove( LockerRoom lockerRoom );
}
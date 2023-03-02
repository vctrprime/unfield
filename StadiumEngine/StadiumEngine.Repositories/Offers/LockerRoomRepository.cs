using Microsoft.EntityFrameworkCore;
using StadiumEngine.Domain.Entities.Offers;
using StadiumEngine.Domain.Repositories.Offers;
using StadiumEngine.Repositories.Infrastructure.Contexts;

namespace StadiumEngine.Repositories.Offers;

internal class LockerRoomRepository : BaseRepository<LockerRoom>, ILockerRoomRepository
{
    public LockerRoomRepository(MainDbContext context) : base(context)
    {
    }

    public async Task<List<LockerRoom>> GetAll(int stadiumId)
    {
        return await Entities.Where(lr => lr.StadiumId == stadiumId && !lr.IsDeleted).ToListAsync();
    }

    public async Task<LockerRoom?> Get(int lockerRoomId,int stadiumId)
    {
        return await Entities.FirstOrDefaultAsync(lr => lr.Id == lockerRoomId && lr.StadiumId == stadiumId && !lr.IsDeleted);
    }

    public new void Add(LockerRoom lockerRoom)
    {
        base.Add(lockerRoom);
    }

    public new void Update(LockerRoom lockerRoom)
    {
        base.Update(lockerRoom);
    }

    public new void Remove(LockerRoom lockerRoom)
    {
        lockerRoom.IsDeleted = true;
        base.Update(lockerRoom);
    }
}
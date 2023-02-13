using Microsoft.EntityFrameworkCore;
using StadiumEngine.Domain.Entities.Offers;
using StadiumEngine.Domain.Repositories.Offers;
using StadiumEngine.Repositories.Infrastructure.Contexts;

namespace StadiumEngine.Repositories.Offers;

internal class FieldSportKindRepository : BaseRepository<FieldSportKind>, IFieldSportKindRepository
{
    public FieldSportKindRepository(MainDbContext context) : base(context)
    {
    }

    public new void Add(IEnumerable<FieldSportKind> sportKinds)
    {
        base.Add(sportKinds);
    }

    public new void Remove(IEnumerable<FieldSportKind> sportKinds)
    {
        base.Remove(sportKinds);
    }
}
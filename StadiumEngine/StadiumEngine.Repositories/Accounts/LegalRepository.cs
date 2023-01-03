using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Repositories.Accounts;
using StadiumEngine.Repositories.Infrastructure.Contexts;

namespace StadiumEngine.Repositories.Accounts;

internal class LegalRepository : BaseRepository<Legal>, ILegalRepository
{
    public LegalRepository(MainDbContext context) : base(context)
    {
    }

    public new void Add(Legal legal)
    {
        base.Add(legal);
    }
}
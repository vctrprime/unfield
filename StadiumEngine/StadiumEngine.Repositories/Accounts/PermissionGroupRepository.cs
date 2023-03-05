using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Repositories.Accounts;
using StadiumEngine.Repositories.Infrastructure.Contexts;

namespace StadiumEngine.Repositories.Accounts;

internal class PermissionGroupRepository : BaseRepository<PermissionGroup>, IPermissionGroupRepository
{
    public PermissionGroupRepository( MainDbContext context ) : base( context )
    {
    }

    public new void Add( PermissionGroup permissionGroup ) => base.Add( permissionGroup );

    public new void Update( PermissionGroup permissionGroup ) => base.Update( permissionGroup );
}
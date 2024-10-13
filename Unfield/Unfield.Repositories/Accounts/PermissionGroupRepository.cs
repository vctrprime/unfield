using Unfield.Domain.Entities.Accounts;
using Unfield.Domain.Repositories.Accounts;
using Unfield.Repositories.Infrastructure.Contexts;

namespace Unfield.Repositories.Accounts;

internal class PermissionGroupRepository : BaseRepository<PermissionGroup>, IPermissionGroupRepository
{
    public PermissionGroupRepository( MainDbContext context ) : base( context )
    {
    }

    public new void Add( PermissionGroup permissionGroup ) => base.Add( permissionGroup );

    public new void Update( PermissionGroup permissionGroup ) => base.Update( permissionGroup );
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Unfield.Domain.Entities;
using Unfield.Domain.Entities.Accounts;

namespace Unfield.Repositories.Infrastructure.Configurations.Accounts;

internal class PermissionGroupConfiguration : BaseEntityConfiguration, IEntityTypeConfiguration<PermissionGroup>
{
    public void Configure( EntityTypeBuilder<PermissionGroup> builder )
    {
        builder.ToTable( "permission_group", "accounts" );
        BaseEntityConfigure( builder );
        
        builder.Property( p => p.Key ).HasColumnName( "key" );
        builder.Property( p => p.Sort ).HasColumnName( "sort" );
        builder.Property( p => p.Name ).HasColumnName( "name" );
        builder.Property( p => p.Description ).HasColumnName( "description" );
    }
}
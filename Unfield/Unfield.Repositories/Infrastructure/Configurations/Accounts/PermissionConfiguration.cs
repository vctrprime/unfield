using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Unfield.Domain.Entities;
using Unfield.Domain.Entities.Accounts;

namespace Unfield.Repositories.Infrastructure.Configurations.Accounts;

internal class PermissionConfiguration : BaseEntityConfiguration, IEntityTypeConfiguration<Permission>
{
    public void Configure( EntityTypeBuilder<Permission> builder )
    {
        builder.ToTable( "permission", "accounts" );
        BaseEntityConfigure( builder );
        
        builder.Property( p => p.DisplayName ).HasColumnName( "display_name" );
        builder.Property( p => p.PermissionGroupId ).HasColumnName( "permission_group_id" );
        builder.Property( p => p.Sort ).HasColumnName( "sort" );
        builder.Property( p => p.Name ).HasColumnName( "name" );
        builder.Property( p => p.Description ).HasColumnName( "description" );

        builder.HasOne( x => x.PermissionGroup )
            .WithMany( x => x.Permissions )
            .HasForeignKey( x => x.PermissionGroupId );
    }
}
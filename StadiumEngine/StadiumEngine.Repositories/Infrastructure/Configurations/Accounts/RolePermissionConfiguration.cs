using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StadiumEngine.Domain.Entities;
using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Repositories.Infrastructure.Configurations.Accounts;

internal class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
{
    public void Configure( EntityTypeBuilder<RolePermission> builder )
    {
        builder.ToTable( "role_permission", "accounts" );
        builder.HasBaseType( typeof( BaseUserEntity ) );
        
        builder.HasOne( x => x.UserCreated )
            .WithMany( x => x.CreatedRolePermissions )
            .HasForeignKey( x => x.UserCreatedId );
        
        builder.HasOne( x => x.UserModified )
            .WithMany( x => x.LastModifiedRolePermissions )
            .HasForeignKey( x => x.UserModifiedId );
        
        builder.Property( p => p.RoleId ).HasColumnName( "role_id" );
        builder.Property( p => p.PermissionId ).HasColumnName( "permission_id" );

        builder.HasOne( x => x.Role )
            .WithMany( x => x.RolePermissions )
            .HasForeignKey( x => x.RoleId );
        builder.HasOne( x => x.Permission )
            .WithMany( x => x.RolePermissions )
            .HasForeignKey( x => x.PermissionId );
    }
}
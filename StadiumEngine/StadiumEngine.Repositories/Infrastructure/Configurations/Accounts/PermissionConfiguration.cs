using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StadiumEngine.Domain.Entities;
using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Repositories.Infrastructure.Configurations.Accounts;

internal class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure( EntityTypeBuilder<Permission> builder )
    {
        builder.ToTable( "permission", "accounts" );
        builder.HasBaseType( typeof( BaseEntity ) );
        
        builder.Property( p => p.DisplayName ).HasColumnName( "display_name" );
        builder.Property( p => p.PermissionGroupId ).HasColumnName( "permission_group_id" );
        builder.Property( p => p.Sort ).HasColumnName( "sort" ).HasDefaultValue(1);
        builder.Property( p => p.Name ).HasColumnName( "name" );
        builder.Property( p => p.Description ).HasColumnName( "description" );

        builder.HasOne( x => x.PermissionGroup )
            .WithMany( x => x.Permissions )
            .HasForeignKey( x => x.PermissionGroupId );
    }
}
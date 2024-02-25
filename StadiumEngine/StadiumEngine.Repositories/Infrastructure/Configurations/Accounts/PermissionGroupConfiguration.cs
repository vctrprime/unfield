using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StadiumEngine.Domain.Entities;
using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Repositories.Infrastructure.Configurations.Accounts;

internal class PermissionGroupConfiguration : IEntityTypeConfiguration<PermissionGroup>
{
    public void Configure( EntityTypeBuilder<PermissionGroup> builder )
    {
        builder.ToTable( "permission_group", "accounts" );
        builder.HasBaseType( typeof( BaseEntity ) );
        
        builder.Property( p => p.Key ).HasColumnName( "permission_group" );
        builder.Property( p => p.Sort ).HasColumnName( "sort" ).HasDefaultValue(1);
        builder.Property( p => p.Name ).HasColumnName( "name" );
        builder.Property( p => p.Description ).HasColumnName( "description" );
    }
}
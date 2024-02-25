using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StadiumEngine.Domain.Entities;
using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Repositories.Infrastructure.Configurations.Accounts;

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
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StadiumEngine.Domain.Entities;
using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Repositories.Infrastructure.Configurations.Accounts;

internal class RoleConfiguration : BaseUserEntityConfiguration, IEntityTypeConfiguration<Role>
{
    public void Configure( EntityTypeBuilder<Role> builder )
    {
        builder.ToTable( "role", "accounts" );
        BaseUserEntityConfigure( builder );
        
        builder.HasOne( x => x.UserCreated )
            .WithMany( x => x.CreatedRoles )
            .HasForeignKey( x => x.UserCreatedId );
        
        builder.HasOne( x => x.UserModified )
            .WithMany( x => x.LastModifiedRoles )
            .HasForeignKey( x => x.UserModifiedId );
        
        builder.Property( p => p.Name ).HasColumnName( "name" );
        builder.Property( p => p.Description ).HasColumnName( "description" );
        builder.Property( p => p.StadiumGroupId ).HasColumnName( "stadium_group_id" );
        builder.Property( p => p.IsDeleted ).HasColumnName( "is_deleted" );

        builder.HasOne( x => x.StadiumGroup )
            .WithMany( x => x.Roles )
            .HasForeignKey( x => x.StadiumGroupId );
    }
}
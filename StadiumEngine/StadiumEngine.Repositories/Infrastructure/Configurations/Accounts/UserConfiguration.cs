using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Repositories.Infrastructure.Configurations.Accounts;

internal class UserConfiguration : BaseUserEntityConfiguration, IEntityTypeConfiguration<User>
{
    public void Configure( EntityTypeBuilder<User> builder )
    {
        builder.ToTable( "user", "accounts" );
        BaseUserEntityConfigure( builder );
        
        builder.HasOne( x => x.UserCreated )
            .WithMany( x => x.CreatedUsers )
            .HasForeignKey( x => x.UserCreatedId );
        
        builder.HasOne( x => x.UserModified )
            .WithMany( x => x.LastModifiedUsers )
            .HasForeignKey( x => x.UserModifiedId );
        
        builder.Property( p => p.Password ).HasColumnName( "password" );
        builder.Property( p => p.PhoneNumber ).HasColumnName( "phone_number" );
        builder.Property( p => p.LastName ).HasColumnName( "last_name" );
        builder.Property( p => p.Name ).HasColumnName( "name" );
        builder.Property( p => p.Description ).HasColumnName( "description" );
        builder.Property( p => p.LegalId ).HasColumnName( "legal_id" );
        builder.Property( p => p.RoleId ).HasColumnName( "role_id" );
        builder.Property( p => p.IsSuperuser ).HasColumnName( "is_superuser" );
        builder.Property( p => p.LastLoginDate ).HasColumnName( "last_login_date" );
        builder.Property( p => p.IsDeleted ).HasColumnName( "is_deleted" );
        builder.Property( p => p.IsAdmin ).HasColumnName( "is_admin" );
        builder.Property( p => p.Language ).HasColumnName( "language" );

        builder.HasIndex( p => p.PhoneNumber ).IsUnique();
        builder.HasOne( x => x.Legal )
            .WithMany( x => x.Users )
            .HasForeignKey( x => x.LegalId );
        builder.HasOne( x => x.Role )
            .WithMany( x => x.Users )
            .HasForeignKey( x => x.RoleId );
    }
}
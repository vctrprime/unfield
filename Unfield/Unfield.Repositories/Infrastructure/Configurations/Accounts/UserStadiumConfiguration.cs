using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Unfield.Domain.Entities;
using Unfield.Domain.Entities.Accounts;

namespace Unfield.Repositories.Infrastructure.Configurations.Accounts;

internal class UserStadiumConfiguration : BaseUserEntityConfiguration, IEntityTypeConfiguration<UserStadium>
{
    public void Configure( EntityTypeBuilder<UserStadium> builder )
    {
        builder.ToTable( "user_stadium", "accounts" );
        BaseUserEntityConfigure( builder );
        
        builder.HasOne( x => x.UserCreated )
            .WithMany( x => x.CreatedUserStadiums )
            .HasForeignKey( x => x.UserCreatedId );
        
        builder.HasOne( x => x.UserModified )
            .WithMany( x => x.LastModifiedUserStadiums )
            .HasForeignKey( x => x.UserModifiedId );
        
        builder.Property( p => p.UserId ).HasColumnName( "user_id" );
        builder.Property( p => p.StadiumId ).HasColumnName( "stadium_id" );

        builder.HasOne( x => x.User )
            .WithMany( x => x.UserStadiums )
            .HasForeignKey( x => x.UserId );
        builder.HasOne( x => x.Stadium )
            .WithMany( x => x.UserStadiums )
            .HasForeignKey( x => x.StadiumId );
    }
}
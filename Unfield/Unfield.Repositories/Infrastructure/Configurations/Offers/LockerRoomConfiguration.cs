using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Unfield.Domain.Entities.Offers;

namespace Unfield.Repositories.Infrastructure.Configurations.Offers;

internal class LockerRoomConfiguration : BaseUserEntityConfiguration, IEntityTypeConfiguration<LockerRoom>
{
    public void Configure( EntityTypeBuilder<LockerRoom> builder )
    {
        builder.ToTable( "locker_room", "offers" );
        BaseUserEntityConfigure( builder );
        
        builder.HasOne( x => x.UserCreated )
            .WithMany( x => x.CreatedLockerRooms )
            .HasForeignKey( x => x.UserCreatedId );
        
        builder.HasOne( x => x.UserModified )
            .WithMany( x => x.LastModifiedLockerRooms )
            .HasForeignKey( x => x.UserModifiedId );
        
        builder.Property( p => p.Name ).HasColumnName( "name" );
        builder.Property( p => p.Description ).HasColumnName( "description" );
        builder.Property( p => p.Gender ).HasColumnName( "gender" ).IsRequired();
        builder.Property( p => p.IsActive ).HasColumnName( "is_active" ).IsRequired();
        builder.Property( p => p.IsDeleted ).HasColumnName( "is_deleted" ).IsRequired();
        builder.Property( p => p.StadiumId ).HasColumnName( "stadium_id" ).IsRequired();
        
        builder.HasOne( x => x.Stadium )
            .WithMany( x => x.LockerRooms )
            .HasForeignKey( x => x.StadiumId );
    }
}
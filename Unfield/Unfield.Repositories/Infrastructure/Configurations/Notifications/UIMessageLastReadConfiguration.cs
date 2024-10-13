using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Unfield.Domain.Entities;
using Unfield.Domain.Entities.Notifications;

namespace Unfield.Repositories.Infrastructure.Configurations.Notifications;

internal class UIMessageLastReadConfiguration : BaseEntityConfiguration, IEntityTypeConfiguration<UIMessageLastRead>
{
    public void Configure( EntityTypeBuilder<UIMessageLastRead> builder )
    {
        builder.ToTable( "ui_message_last_read", "notifications" );
        BaseEntityConfigure( builder );
        
        builder.Property( p => p.StadiumId ).HasColumnName( "stadium_id" );
        builder.Property( p => p.UserId ).HasColumnName( "user_id" );
        builder.Property( p => p.MessageId ).HasColumnName( "ui_message_id" );
        
        builder.HasOne( x => x.Stadium )
            .WithMany( x => x.UIMessageLastReads )
            .HasForeignKey( x => x.StadiumId );
        builder.HasOne( x => x.User )
            .WithMany( x => x.UIMessageLastReads )
            .HasForeignKey( x => x.UserId );
        builder.HasOne( x => x.Message )
            .WithMany( x => x.UIMessageLastReads )
            .HasForeignKey( x => x.MessageId );
    }
}
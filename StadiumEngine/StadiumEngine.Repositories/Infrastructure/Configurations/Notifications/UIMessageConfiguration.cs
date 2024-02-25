using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StadiumEngine.Domain.Entities;
using StadiumEngine.Domain.Entities.Notifications;

namespace StadiumEngine.Repositories.Infrastructure.Configurations.Notifications;

internal class UIMessageConfiguration : IEntityTypeConfiguration<UIMessage>
{
    public void Configure( EntityTypeBuilder<UIMessage> builder )
    {
        builder.ToTable( "ui_message", "notifications" );
        builder.HasBaseType( typeof( BaseEntity ) );
        
        builder.Property( p => p.StadiumId ).HasColumnName( "stadium_id" );
        builder.Property( p => p.MessageType ).HasColumnName( "message_type" );
        
        builder.HasOne( x => x.Stadium )
            .WithMany( x => x.UIMessages )
            .HasForeignKey( x => x.StadiumId );
    }
}
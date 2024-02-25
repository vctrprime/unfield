using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StadiumEngine.Domain.Entities;
using StadiumEngine.Domain.Entities.Notifications;

namespace StadiumEngine.Repositories.Infrastructure.Configurations.Notifications;

internal class UIMessageTextConfiguration : IEntityTypeConfiguration<UIMessageText>
{
    public void Configure( EntityTypeBuilder<UIMessageText> builder )
    {
        builder.ToTable( "ui_message_text", "notifications" );
        builder.HasBaseType( typeof( BaseEntity ) );
        
        builder.Property( p => p.MessageId ).HasColumnName( "ui_message_id" );
        builder.Property( p => p.Text ).HasColumnName( "text" );
        builder.Property( p => p.Index ).HasColumnName( "index" );
        
        builder.HasOne( x => x.Message )
            .WithMany( x => x.Texts )
            .HasForeignKey( x => x.MessageId );
    }
}
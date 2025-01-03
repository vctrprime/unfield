using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Unfield.Domain.Entities;
using Unfield.Domain.Entities.Notifications;

namespace Unfield.Repositories.Infrastructure.Configurations.Notifications;

internal class UIMessageTextConfiguration : BaseEntityConfiguration, IEntityTypeConfiguration<UIMessageText>
{
    public void Configure( EntityTypeBuilder<UIMessageText> builder )
    {
        builder.ToTable( "ui_message_text", "notifications" );
        BaseEntityConfigure( builder, ignoreDateCreated: true, ignoreDateModified: true );
        
        builder.Property( p => p.MessageId ).HasColumnName( "ui_message_id" );
        builder.Property( p => p.Text ).HasColumnName( "text" );
        builder.Property( p => p.Index ).HasColumnName( "index" );
        
        builder.HasOne( x => x.Message )
            .WithMany( x => x.Texts )
            .HasForeignKey( x => x.MessageId );
    }
}
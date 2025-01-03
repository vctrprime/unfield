using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Unfield.Domain.Entities;
using Unfield.Domain.Entities.Settings;

namespace Unfield.Repositories.Infrastructure.Configurations.Settings;

internal class MainSettingsConfiguration : BaseUserEntityConfiguration, IEntityTypeConfiguration<MainSettings>
{
    public void Configure( EntityTypeBuilder<MainSettings> builder )
    {
        builder.ToTable( "main_settings", "settings" );
        BaseUserEntityConfigure( builder, ignoreUserCreated: true );

        builder.HasOne( x => x.UserModified )
            .WithMany( x => x.LastModifiedMainSettings )
            .HasForeignKey( x => x.UserModifiedId );
        
        builder.Property( p => p.Name ).HasColumnName( "name" );
        builder.Property( p => p.Description ).HasColumnName( "description" );
        builder.Property( p => p.StadiumId ).HasColumnName( "stadium_id" );
        builder.Property( p => p.OpenTime ).HasColumnName( "open_time" );
        builder.Property( p => p.CloseTime ).HasColumnName( "close_time" );
        
        builder.HasOne( x => x.Stadium )
            .WithOne( x => x.MainSettings )
            .HasForeignKey<MainSettings>( x => x.StadiumId );
    }
}
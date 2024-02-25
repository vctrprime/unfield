using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StadiumEngine.Domain.Entities;
using StadiumEngine.Domain.Entities.Settings;

namespace StadiumEngine.Repositories.Infrastructure.Configurations.Settings;

internal class BreakConfiguration : BaseUserEntityConfiguration, IEntityTypeConfiguration<Break>
{
    public void Configure( EntityTypeBuilder<Break> builder )
    {
        builder.ToTable( "break", "settings" );
        BaseUserEntityConfigure( builder );
        
        builder.HasOne( x => x.UserCreated )
            .WithMany( x => x.CreatedBreaks )
            .HasForeignKey( x => x.UserCreatedId );
        
        builder.HasOne( x => x.UserModified )
            .WithMany( x => x.LastModifiedBreaks )
            .HasForeignKey( x => x.UserModifiedId );
        
        builder.Property( p => p.Name ).HasColumnName( "name" );
        builder.Property( p => p.Description ).HasColumnName( "description" );
        builder.Property( p => p.StadiumId ).HasColumnName( "stadium_id" );
        builder.Property( p => p.StartHour ).HasColumnName( "start_hour" );
        builder.Property( p => p.EndHour ).HasColumnName( "end_hour" );
        builder.Property( p => p.IsActive ).HasColumnName( "is_active" );
        builder.Property( p => p.IsDeleted ).HasColumnName( "is_deleted" );
        builder.Property( p => p.DateStart ).HasColumnName( "date_start" ).HasColumnType( "timestamp without time zone" );
        builder.Property( p => p.DateEnd ).HasColumnName( "date_end" ).HasColumnType( "timestamp without time zone" );
        
        builder.HasOne( x => x.Stadium )
            .WithMany( x => x.Breaks )
            .HasForeignKey( x => x.StadiumId );
    }
}
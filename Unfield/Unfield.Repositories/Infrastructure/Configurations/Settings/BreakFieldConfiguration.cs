using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Unfield.Domain.Entities;
using Unfield.Domain.Entities.Settings;

namespace Unfield.Repositories.Infrastructure.Configurations.Settings;

internal class BreakFieldConfiguration : BaseUserEntityConfiguration, IEntityTypeConfiguration<BreakField>
{
    public void Configure( EntityTypeBuilder<BreakField> builder )
    {
        builder.ToTable( "break_field", "settings" );
        BaseUserEntityConfigure( builder );
        
        builder.HasOne( x => x.UserCreated )
            .WithMany( x => x.CreatedBreakFields )
            .HasForeignKey( x => x.UserCreatedId );
        
        builder.HasOne( x => x.UserModified )
            .WithMany( x => x.LastModifiedBreakFields )
            .HasForeignKey( x => x.UserModifiedId );
        
        builder.Property( p => p.BreakId ).HasColumnName( "break_id" );
        builder.Property( p => p.FieldId ).HasColumnName( "field_id" );
        
        builder.HasOne( x => x.Field )
            .WithMany( x => x.BreakFields )
            .HasForeignKey( x => x.FieldId );
        
        builder.HasOne( x => x.Break )
            .WithMany( x => x.BreakFields )
            .HasForeignKey( x => x.BreakId );
    }
}
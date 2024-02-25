using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StadiumEngine.Domain.Entities;
using StadiumEngine.Domain.Entities.Offers;

namespace StadiumEngine.Repositories.Infrastructure.Configurations.Offers;

internal class FieldConfiguration : BaseOfferEntityConfiguration, IEntityTypeConfiguration<Field>
{
    public void Configure( EntityTypeBuilder<Field> builder )
    {
        builder.ToTable( "field", "offers" );
        BaseOfferEntityConfigure( builder );
        
        builder.HasOne( x => x.UserCreated )
            .WithMany( x => x.CreatedFields )
            .HasForeignKey( x => x.UserCreatedId );
        
        builder.HasOne( x => x.UserModified )
            .WithMany( x => x.LastModifiedFields )
            .HasForeignKey( x => x.UserModifiedId );
        
        builder.HasOne( x => x.Stadium )
            .WithMany( x => x.Fields )
            .HasForeignKey( x => x.StadiumId );

        builder.Property( p => p.ParentFieldId ).HasColumnName( "parent_field_id" );
        builder.Property( p => p.CoveringType ).HasColumnName( "covering_type" );
        builder.Property( p => p.Width ).HasColumnName( "width" );
        builder.Property( p => p.Length ).HasColumnName( "length" );
        builder.Property( p => p.PriceGroupId ).HasColumnName( "price_group_id" );
        
        builder.HasOne( x => x.ParentField )
            .WithMany( x => x.ChildFields )
            .HasForeignKey( x => x.ParentFieldId );
        builder.HasOne( x => x.PriceGroup )
            .WithMany( x => x.Fields )
            .HasForeignKey( x => x.PriceGroupId );
    }
}
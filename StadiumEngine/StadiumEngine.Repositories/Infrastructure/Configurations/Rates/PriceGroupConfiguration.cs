using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StadiumEngine.Domain.Entities.Rates;

namespace StadiumEngine.Repositories.Infrastructure.Configurations.Rates;

internal class PriceGroupConfiguration : BaseRateEntityConfiguration, IEntityTypeConfiguration<PriceGroup>
{
    public void Configure( EntityTypeBuilder<PriceGroup> builder )
    {
        builder.ToTable( "price_group", "rates" );
        BaseRateEntityConfigure( builder );
        
        builder.HasOne( x => x.UserCreated )
            .WithMany( x => x.CreatedPriceGroups )
            .HasForeignKey( x => x.UserCreatedId );
        
        builder.HasOne( x => x.UserModified )
            .WithMany( x => x.LastModifiedPriceGroups )
            .HasForeignKey( x => x.UserModifiedId );
        
        builder.HasOne( x => x.Stadium )
            .WithMany( x => x.PriceGroups )
            .HasForeignKey( x => x.StadiumId );
    }
}
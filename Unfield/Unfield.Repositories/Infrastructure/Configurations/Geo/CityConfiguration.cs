using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Unfield.Domain.Entities;
using Unfield.Domain.Entities.Geo;

namespace Unfield.Repositories.Infrastructure.Configurations.Geo;

internal class CityConfiguration : BaseGeoEntityConfiguration, IEntityTypeConfiguration<City>
{
    public void Configure( EntityTypeBuilder<City> builder )
    {
        builder.ToTable( "city", "geo" );
        BaseGeoEntityConfigure( builder );
        
        builder.Property( p => p.RegionId ).HasColumnName( "region_id" );
        
        builder.HasOne( x => x.Region )
            .WithMany( x => x.Cities )
            .HasForeignKey( x => x.RegionId );
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Unfield.Domain.Entities;
using Unfield.Domain.Entities.Geo;

namespace Unfield.Repositories.Infrastructure.Configurations.Geo;

internal class RegionConfiguration : BaseGeoEntityConfiguration, IEntityTypeConfiguration<Region>
{
    public void Configure( EntityTypeBuilder<Region> builder )
    {
        builder.ToTable( "region", "geo" );
        BaseGeoEntityConfigure( builder );
        
        builder.Property( p => p.CountryId ).HasColumnName( "country_id" );
        
        builder.HasOne( x => x.Country )
            .WithMany( x => x.Regions )
            .HasForeignKey( x => x.CountryId );
    }
}
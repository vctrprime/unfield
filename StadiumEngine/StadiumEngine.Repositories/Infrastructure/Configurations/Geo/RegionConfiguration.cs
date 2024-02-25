using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StadiumEngine.Domain.Entities;
using StadiumEngine.Domain.Entities.Geo;

namespace StadiumEngine.Repositories.Infrastructure.Configurations.Geo;

internal class RegionConfiguration : IEntityTypeConfiguration<Region>
{
    public void Configure( EntityTypeBuilder<Region> builder )
    {
        builder.ToTable( "region", "geo" );
        builder.HasBaseType( typeof( BaseGeoEntity ) );
        
        builder.Property( p => p.CountryId ).HasColumnName( "country_id" );
        
        builder.HasOne( x => x.Country )
            .WithMany( x => x.Regions )
            .HasForeignKey( x => x.CountryId );
    }
}
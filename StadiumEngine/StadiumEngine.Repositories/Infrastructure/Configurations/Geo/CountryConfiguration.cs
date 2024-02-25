using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StadiumEngine.Domain.Entities.Geo;

namespace StadiumEngine.Repositories.Infrastructure.Configurations.Geo;

internal class CountryConfiguration : BaseGeoEntityConfiguration, IEntityTypeConfiguration<Country>
{
    public void Configure( EntityTypeBuilder<Country> builder )
    {
        builder.ToTable( "country", "geo" );
        BaseGeoEntityConfigure( builder );
    }
}
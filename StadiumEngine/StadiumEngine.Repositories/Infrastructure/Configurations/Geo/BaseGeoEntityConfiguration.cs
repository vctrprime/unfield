using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StadiumEngine.Domain.Entities;
using StadiumEngine.Domain.Entities.Geo;

namespace StadiumEngine.Repositories.Infrastructure.Configurations.Geo;

internal class BaseGeoEntityConfiguration : IEntityTypeConfiguration<BaseGeoEntity>
{
    public void Configure( EntityTypeBuilder<BaseGeoEntity> builder )
    {
        builder.HasBaseType( typeof( BaseEntity ) );
        
        builder.Property( p => p.Name ).HasColumnName( "name" );
        builder.Property( p => p.Description ).HasColumnName( "description" );
        builder.Property( p => p.ShortName ).HasColumnName( "short_name" );
    }
}
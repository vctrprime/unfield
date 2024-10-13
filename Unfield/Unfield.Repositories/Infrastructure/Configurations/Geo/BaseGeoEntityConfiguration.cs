using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Unfield.Domain.Entities.Geo;

namespace Unfield.Repositories.Infrastructure.Configurations.Geo;

internal abstract class BaseGeoEntityConfiguration : BaseEntityConfiguration
{
    protected void BaseGeoEntityConfigure<T>( EntityTypeBuilder<T> builder ) where T : BaseGeoEntity
    {
        BaseEntityConfigure( builder );
        
        builder.Property( p => p.Name ).HasColumnName( "name" );
        builder.Property( p => p.Description ).HasColumnName( "description" );
        builder.Property( p => p.ShortName ).HasColumnName( "short_name" );
    }
}
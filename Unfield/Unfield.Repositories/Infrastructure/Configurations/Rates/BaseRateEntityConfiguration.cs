using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Unfield.Domain.Entities;
using Unfield.Domain.Entities.Rates;

namespace Unfield.Repositories.Infrastructure.Configurations.Rates;

internal abstract class BaseRateEntityConfiguration : BaseUserEntityConfiguration
{
    protected void BaseRateEntityConfigure<T>( EntityTypeBuilder<T> builder ) where T : BaseRateEntity
    {
        BaseUserEntityConfigure( builder );
        
        builder.Property( p => p.Name ).HasColumnName( "name" );
        builder.Property( p => p.Description ).HasColumnName( "description" );
        builder.Property( p => p.StadiumId ).HasColumnName( "stadium_id" );
        builder.Property( p => p.IsActive ).HasColumnName( "is_active" );
        builder.Property( p => p.IsDeleted ).HasColumnName( "is_deleted" );
    }
}
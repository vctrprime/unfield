using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Unfield.Domain.Entities.Offers;

namespace Unfield.Repositories.Infrastructure.Configurations.Offers;

internal abstract class BaseOfferEntityConfiguration : BaseUserEntityConfiguration
{
    protected void BaseOfferEntityConfigure<T>( EntityTypeBuilder<T> builder ) where T : BaseOfferEntity
    {
        BaseUserEntityConfigure( builder );
        
        builder.Property( p => p.Name ).HasColumnName( "name" );
        builder.Property( p => p.Description ).HasColumnName( "description" );
        builder.Property( p => p.StadiumId ).HasColumnName( "stadium_id" );
        builder.Property( p => p.IsActive ).HasColumnName( "is_active" );
        builder.Property( p => p.IsDeleted ).HasColumnName( "is_deleted" );
    }
}
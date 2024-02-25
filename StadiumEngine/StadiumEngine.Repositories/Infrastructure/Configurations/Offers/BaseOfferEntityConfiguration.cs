using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StadiumEngine.Domain.Entities;
using StadiumEngine.Domain.Entities.Offers;

namespace StadiumEngine.Repositories.Infrastructure.Configurations.Offers;

internal class BaseOfferEntityConfiguration : IEntityTypeConfiguration<BaseOfferEntity>
{
    public void Configure( EntityTypeBuilder<BaseOfferEntity> builder )
    {
        builder.HasBaseType( typeof( BaseUserEntity ) );
        
        builder.Property( p => p.Name ).HasColumnName( "name" );
        builder.Property( p => p.Description ).HasColumnName( "description" );
        builder.Property( p => p.StadiumId ).HasColumnName( "stadium_id" );
        builder.Property( p => p.IsActive ).HasColumnName( "is_active" );
        builder.Property( p => p.IsDeleted ).HasColumnName( "is_deleted" );
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Unfield.Domain.Entities;
using Unfield.Domain.Entities.Rates;

namespace Unfield.Repositories.Infrastructure.Configurations.Rates;

internal class PromoCodeConfiguration : BaseUserEntityConfiguration, IEntityTypeConfiguration<PromoCode>
{
    public void Configure( EntityTypeBuilder<PromoCode> builder )
    {
        builder.ToTable( "promo_code", "rates" );
        BaseUserEntityConfigure( builder );
        
        builder.HasOne( x => x.UserCreated )
            .WithMany( x => x.CreatedPromoCodes )
            .HasForeignKey( x => x.UserCreatedId );
        
        builder.HasOne( x => x.UserModified )
            .WithMany( x => x.LastModifiedPromoCodes )
            .HasForeignKey( x => x.UserModifiedId );
        
        builder.Property( p => p.Code ).HasColumnName( "code" );
        builder.Property( p => p.Type ).HasColumnName( "type" );
        builder.Property( p => p.TariffId ).HasColumnName( "tariff_id" );
        builder.Property( p => p.Value ).HasColumnName( "value" );
        
        builder.HasOne( x => x.Tariff )
            .WithMany( x => x.PromoCodes )
            .HasForeignKey( x => x.TariffId );
    }
}
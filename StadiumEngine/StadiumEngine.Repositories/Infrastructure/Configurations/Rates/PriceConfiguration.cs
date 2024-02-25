using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StadiumEngine.Domain.Entities;
using StadiumEngine.Domain.Entities.Rates;

namespace StadiumEngine.Repositories.Infrastructure.Configurations.Rates;

internal class PriceConfiguration : BaseUserEntityConfiguration, IEntityTypeConfiguration<Price>
{
    public void Configure( EntityTypeBuilder<Price> builder )
    {
        builder.ToTable( "price", "rates" );
        BaseUserEntityConfigure( builder );
        
        builder.HasOne( x => x.UserCreated )
            .WithMany( x => x.CreatedPrices )
            .HasForeignKey( x => x.UserCreatedId );
        
        builder.HasOne( x => x.UserModified )
            .WithMany( x => x.LastModifiedPrices )
            .HasForeignKey( x => x.UserModifiedId );
        
        builder.Property( p => p.FieldId ).HasColumnName( "field_id" );
        builder.Property( p => p.TariffDayIntervalId ).HasColumnName( "tariff_day_interval_id" );
        builder.Property( p => p.IsObsolete ).HasColumnName( "is_obsolete" );
        builder.Property( p => p.Currency ).HasColumnName( "currency" );
        builder.Property( p => p.Value ).HasColumnName( "value" );
        
        builder.HasOne( x => x.Field )
            .WithMany( x => x.Prices )
            .HasForeignKey( x => x.FieldId );
        
        builder.HasOne( x => x.TariffDayInterval )
            .WithMany( x => x.Prices )
            .HasForeignKey( x => x.TariffDayIntervalId );
    }
}
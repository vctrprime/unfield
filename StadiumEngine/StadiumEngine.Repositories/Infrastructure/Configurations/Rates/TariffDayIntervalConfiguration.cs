using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StadiumEngine.Domain.Entities;
using StadiumEngine.Domain.Entities.Rates;

namespace StadiumEngine.Repositories.Infrastructure.Configurations.Rates;

internal class TariffDayIntervalConfiguration : BaseUserEntityConfiguration, IEntityTypeConfiguration<TariffDayInterval>
{
    public void Configure( EntityTypeBuilder<TariffDayInterval> builder )
    {
        builder.ToTable( "tariff_day_interval", "rates" );
        BaseUserEntityConfigure( builder );
        
        builder.HasOne( x => x.UserCreated )
            .WithMany( x => x.CreatedTariffDayIntervals )
            .HasForeignKey( x => x.UserCreatedId );
        
        builder.HasOne( x => x.UserModified )
            .WithMany( x => x.LastModifiedTariffDayIntervals )
            .HasForeignKey( x => x.UserModifiedId );
        
        builder.Property( p => p.TariffId ).HasColumnName( "tariff_id" );
        builder.Property( p => p.DayIntervalId ).HasColumnName( "day_interval_id" );
        
        builder.HasOne( x => x.Tariff )
            .WithMany( x => x.TariffDayIntervals )
            .HasForeignKey( x => x.TariffId );
        
        builder.HasOne( x => x.DayInterval )
            .WithMany( x => x.TariffDayIntervals )
            .HasForeignKey( x => x.DayIntervalId );
    }
}
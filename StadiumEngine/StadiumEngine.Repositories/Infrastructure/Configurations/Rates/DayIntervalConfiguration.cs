using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StadiumEngine.Domain.Entities;
using StadiumEngine.Domain.Entities.Rates;

namespace StadiumEngine.Repositories.Infrastructure.Configurations.Rates;

internal class DayIntervalConfiguration : IEntityTypeConfiguration<DayInterval>
{
    public void Configure( EntityTypeBuilder<DayInterval> builder )
    {
        builder.ToTable( "day_interval", "rates" );
        builder.HasBaseType( typeof( BaseEntity ) );
        
        builder.Property( p => p.Start ).HasColumnName( "start" );
        builder.Property( p => p.End ).HasColumnName( "end" );
    }
}
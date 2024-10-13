using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Unfield.Domain.Entities;
using Unfield.Domain.Entities.Rates;

namespace Unfield.Repositories.Infrastructure.Configurations.Rates;

internal class DayIntervalConfiguration : BaseEntityConfiguration, IEntityTypeConfiguration<DayInterval>
{
    public void Configure( EntityTypeBuilder<DayInterval> builder )
    {
        builder.ToTable( "day_interval", "rates" );
        BaseEntityConfigure( builder, ignoreDateModified: true );
        
        builder.Property( p => p.Start ).HasColumnName( "start" );
        builder.Property( p => p.End ).HasColumnName( "end" );
    }
}
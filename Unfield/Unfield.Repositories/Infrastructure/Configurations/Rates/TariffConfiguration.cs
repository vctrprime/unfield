using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Unfield.Domain.Entities.Rates;

namespace Unfield.Repositories.Infrastructure.Configurations.Rates;

internal class TariffConfiguration : BaseRateEntityConfiguration, IEntityTypeConfiguration<Tariff>
{
    public void Configure( EntityTypeBuilder<Tariff> builder )
    {
        builder.ToTable( "tariff", "rates" );
        BaseRateEntityConfigure( builder );
        
        builder.HasOne( x => x.UserCreated )
            .WithMany( x => x.CreatedTariffs )
            .HasForeignKey( x => x.UserCreatedId );
        
        builder.HasOne( x => x.UserModified )
            .WithMany( x => x.LastModifiedTariffs )
            .HasForeignKey( x => x.UserModifiedId );
        
        builder.HasOne( x => x.Stadium )
            .WithMany( x => x.Tariffs )
            .HasForeignKey( x => x.StadiumId );

        builder.Property( p => p.DateStart ).HasColumnName( "date_start" );
        builder.Property( p => p.DateEnd ).HasColumnName( "date_end" );
        builder.Property( p => p.Monday ).HasColumnName( "monday" );
        builder.Property( p => p.Tuesday ).HasColumnName( "tuesday" );
        builder.Property( p => p.Wednesday ).HasColumnName( "wednesday" );
        builder.Property( p => p.Thursday ).HasColumnName( "thursday" );
        builder.Property( p => p.Friday ).HasColumnName( "friday" );
        builder.Property( p => p.Saturday ).HasColumnName( "saturday" );
        builder.Property( p => p.Sunday ).HasColumnName( "sunday" );
    }
}
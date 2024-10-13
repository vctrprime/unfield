using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Unfield.Domain.Entities.Dashboard;

namespace Unfield.Repositories.Infrastructure.Configurations.Dashboard;

internal class StadiumDashboardConfiguration : BaseEntityConfiguration, IEntityTypeConfiguration<StadiumDashboard>
{
    public void Configure( EntityTypeBuilder<StadiumDashboard> builder )
    {
        builder.ToTable( "stadium_dashboard", "dashboards" );
        BaseEntityConfigure( builder );
        
        builder.Property( p => p.StadiumId ).HasColumnName( "stadium_id" ).IsRequired();
        
        builder.OwnsOne(
            d => d.Data,
            b =>
            {
                b.ToJson("data");
                b.OwnsOne( d => d.YearChart, onb => onb.OwnsMany( yc => yc.Items  ) );
                b.OwnsOne( d => d.FieldDistribution, onb => onb.OwnsMany( fd => fd.Items  ) );
                b.OwnsOne( d => d.AverageBill );
                b.OwnsOne( d => d.PopularInventory, onb => onb.OwnsMany( pi => pi.Items  ) );
                b.OwnsOne( d => d.TimeChart, onb => onb.OwnsMany( tc => tc.Items  ) );
            } );
    }
}
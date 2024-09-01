using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StadiumEngine.Domain.Entities;
using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Repositories.Infrastructure.Configurations.Accounts;

internal class StadiumConfiguration : BaseEntityConfiguration, IEntityTypeConfiguration<Stadium>
{
    public void Configure( EntityTypeBuilder<Stadium> builder )
    {
        builder.ToTable( "stadium", "accounts" );
        BaseEntityConfigure( builder );

        builder.Property( p => p.Address ).HasColumnName( "address" );
        builder.Property( p => p.Token ).HasColumnName( "token" );
        builder.Property( p => p.CityId ).HasColumnName( "city_id" ).IsRequired();
        builder.Property( p => p.StadiumGroupId ).HasColumnName( "stadium_group_id" ).IsRequired();
        builder.Property( p => p.Name ).HasColumnName( "name" );
        builder.Property( p => p.Description ).HasColumnName( "description" );
        builder.Property( p => p.IsDeleted ).HasColumnName( "is_deleted" );

        builder.HasOne( x => x.City )
            .WithMany( x => x.Stadiums )
            .HasForeignKey( x => x.CityId );
        builder.HasOne( x => x.StadiumGroup )
            .WithMany( x => x.Stadiums )
            .HasForeignKey( x => x.StadiumGroupId );
    }
}
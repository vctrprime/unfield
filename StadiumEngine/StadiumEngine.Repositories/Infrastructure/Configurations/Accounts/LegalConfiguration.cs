using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Repositories.Infrastructure.Configurations.Accounts;

internal class LegalConfiguration : BaseEntityConfiguration, IEntityTypeConfiguration<Legal>
{
    public void Configure( EntityTypeBuilder<Legal> builder )
    {
        builder.ToTable( "legal", "accounts" );
        BaseEntityConfigure( builder );
        
        builder.Property( p => p.Inn ).HasColumnName( "inn" );
        builder.Property( p => p.HeadName ).HasColumnName( "head_name" );
        builder.Property( p => p.CityId ).HasColumnName( "city_id" );
        builder.Property( p => p.Name ).HasColumnName( "name" );
        builder.Property( p => p.Description ).HasColumnName( "description" );

        builder.HasIndex( p => p.Inn ).IsUnique();
        builder.HasOne( x => x.City )
            .WithMany( x => x.Legals )
            .HasForeignKey( x => x.CityId );
    }
}
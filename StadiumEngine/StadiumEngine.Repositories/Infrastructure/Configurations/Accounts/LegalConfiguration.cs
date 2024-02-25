using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StadiumEngine.Domain.Entities;
using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Repositories.Infrastructure.Configurations.Accounts;

internal class LegalConfiguration : IEntityTypeConfiguration<Legal>
{
    public void Configure( EntityTypeBuilder<Legal> builder )
    {
        builder.ToTable( "legal", "accounts" );
        builder.HasBaseType( typeof( BaseEntity ) );
        
        builder.Property( p => p.Inn ).HasColumnName( "inn" ).IsRequired();
        builder.Property( p => p.HeadName ).HasColumnName( "head_name" ).IsRequired();
        builder.Property( p => p.CityId ).HasColumnName( "city_id" ).IsRequired();
        builder.Property( p => p.Name ).HasColumnName( "name" );
        builder.Property( p => p.Description ).HasColumnName( "description" );

        builder.HasIndex( p => p.Inn ).IsUnique();
        builder.HasOne( x => x.City )
            .WithMany( x => x.Legals )
            .HasForeignKey( x => x.CityId );
    }
}
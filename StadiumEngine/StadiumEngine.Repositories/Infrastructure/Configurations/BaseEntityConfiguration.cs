using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StadiumEngine.Domain.Entities;

namespace StadiumEngine.Repositories.Infrastructure.Configurations;

internal class BaseEntityConfiguration : IEntityTypeConfiguration<BaseEntity>
{
    public void Configure( EntityTypeBuilder<BaseEntity> builder )
    {
        builder.HasKey( p => p.Id );

        builder.Property( p => p.Id ).HasColumnName( "id" );
        builder.Property( p => p.DateCreated )
            .HasColumnName( "date_created" )
            .ValueGeneratedOnAdd()
            .IsRequired()
            .HasDefaultValueSql("now()");
        builder.Property( p => p.DateModified ).HasColumnName( "date_modified" );
    }
}
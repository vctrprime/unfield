using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StadiumEngine.Domain.Entities;

namespace StadiumEngine.Repositories.Infrastructure.Configurations;

internal abstract class BaseEntityConfiguration
{
    protected void BaseEntityConfigure<T>( EntityTypeBuilder<T> builder ) where T : BaseEntity
    {
        builder.HasKey( p => p.Id );

        builder.Property( p => p.Id ).HasColumnName( "id" ).HasColumnOrder( 0 );
        builder.Property( p => p.DateCreated )
            .HasColumnName( "date_created" )
            .ValueGeneratedOnAdd()
            .IsRequired()
            .HasDefaultValueSql( "now()" );
        builder.Property( p => p.DateModified ).HasColumnName( "date_modified" );
    }
}
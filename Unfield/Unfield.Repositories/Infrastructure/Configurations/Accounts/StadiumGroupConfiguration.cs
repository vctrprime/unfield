using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Unfield.Domain.Entities.Accounts;

namespace Unfield.Repositories.Infrastructure.Configurations.Accounts;

internal class StadiumGroupConfiguration : BaseEntityConfiguration, IEntityTypeConfiguration<StadiumGroup>
{
    public void Configure( EntityTypeBuilder<StadiumGroup> builder )
    {
        builder.ToTable( "stadium_group", "accounts" );
        BaseEntityConfigure( builder );
        
        builder.Property( p => p.Name ).HasColumnName( "name" );
        builder.Property( p => p.Description ).HasColumnName( "description" );
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Unfield.Domain.Entities.Customers;

namespace Unfield.Repositories.Infrastructure.Configurations.Customers;

internal class CustomerConfiguration : BaseEntityConfiguration, IEntityTypeConfiguration<Customer>
{
    public void Configure( EntityTypeBuilder<Customer> builder )
    {
        builder.ToTable( "customer", "customers" );
        BaseEntityConfigure( builder );

        builder.Property( p => p.FirstName ).HasColumnName( "first_name" );
        builder.Property( p => p.LastName ).HasColumnName( "last_name" );
        builder.Property( p => p.PhoneNumber ).HasColumnName( "phone_number" ).IsRequired();
        builder.Property( p => p.Language ).HasColumnName( "language" ).IsRequired();
        builder.Property( p => p.Password ).HasColumnName( "password" ).IsRequired();
        builder.Property( p => p.StadiumGroupId ).HasColumnName( "stadium_group_id" ).IsRequired();
        builder.Property( p => p.LastLoginDate ).HasColumnName( "last_login_date" );

        builder.HasIndex(
            c => new
            {
                c.StadiumGroupId,
                c.PhoneNumber
            } )
            .IsUnique();
    }
}
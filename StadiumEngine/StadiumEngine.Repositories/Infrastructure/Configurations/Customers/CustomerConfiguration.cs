using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StadiumEngine.Domain.Entities.Customers;

namespace StadiumEngine.Repositories.Infrastructure.Configurations.Customers;

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
        
    }
}
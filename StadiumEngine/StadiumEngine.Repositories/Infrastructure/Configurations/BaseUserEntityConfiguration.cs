using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StadiumEngine.Domain.Entities;

namespace StadiumEngine.Repositories.Infrastructure.Configurations;

internal class BaseUserEntityConfiguration : IEntityTypeConfiguration<BaseUserEntity>
{
    public void Configure( EntityTypeBuilder<BaseUserEntity> builder )
    {
        builder.HasBaseType( typeof( BaseEntity ) );
        
        builder.Property( p => p.UserCreatedId ).HasColumnName( "user_created_id" );
        builder.Property( p => p.UserModifiedId ).HasColumnName( "user_created_id" );
    }
}
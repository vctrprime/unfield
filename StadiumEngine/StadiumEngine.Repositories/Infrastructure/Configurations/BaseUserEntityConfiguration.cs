using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StadiumEngine.Domain.Entities;

namespace StadiumEngine.Repositories.Infrastructure.Configurations;

internal abstract class BaseUserEntityConfiguration : BaseEntityConfiguration
{
    protected void BaseUserEntityConfigure<T>( EntityTypeBuilder<T> builder ) where T : BaseUserEntity
    {
        BaseEntityConfigure( builder );
        
        builder.Property( p => p.UserCreatedId ).HasColumnName( "user_created_id" );
        builder.Property( p => p.UserModifiedId ).HasColumnName( "user_modified_id" );
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Unfield.Domain.Entities;

namespace Unfield.Repositories.Infrastructure.Configurations;

internal abstract class BaseUserEntityConfiguration : BaseEntityConfiguration
{
    protected void BaseUserEntityConfigure<T>( EntityTypeBuilder<T> builder, bool ignoreUserCreated = false ) where T : BaseUserEntity
    {
        BaseEntityConfigure( builder );

        if ( !ignoreUserCreated )
        {
            builder.Property( p => p.UserCreatedId ).HasColumnName( "user_created_id" );
        }
        
        builder.Property( p => p.UserModifiedId ).HasColumnName( "user_modified_id" );
    }
}
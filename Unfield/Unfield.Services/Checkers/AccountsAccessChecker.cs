using Unfield.Common;
using Unfield.Common.Exceptions;
using Unfield.Domain.Entities.Accounts;

namespace Unfield.Services.Checkers;

internal class AccountsAccessChecker : IAccountsAccessChecker
{
    public void CheckUserAccess( User? user, int stadiumGroupId )
    {
        if ( user == null || stadiumGroupId != user.StadiumGroupId )
        {
            throw new DomainException( ErrorsKeys.UserNotFound );
        }
    }

    public void CheckRoleAccess( Role? role, int stadiumGroupId )
    {
        if ( role == null || stadiumGroupId != role.StadiumGroupId )
        {
            throw new DomainException( ErrorsKeys.RoleNotFound );
        }
    }
}
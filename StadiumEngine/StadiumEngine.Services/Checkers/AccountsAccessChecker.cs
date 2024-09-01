using StadiumEngine.Common;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Services.Checkers;

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
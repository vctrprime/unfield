using StadiumEngine.Common;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Services.Checkers;

internal class AccountsAccessChecker : IAccountsAccessChecker
{
    public void CheckUserAccess( User? user, int legalId )
    {
        if (user == null || legalId != user.LegalId) throw new DomainException( ErrorsKeys.UserNotFound );
    }

    public void CheckRoleAccess( Role? role, int legalId )
    {
        if (role == null || legalId != role.LegalId) throw new DomainException( ErrorsKeys.RoleNotFound );
    }
}
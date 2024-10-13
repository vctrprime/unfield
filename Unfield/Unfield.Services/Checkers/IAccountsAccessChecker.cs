using Unfield.Domain.Entities.Accounts;

namespace Unfield.Services.Checkers;

internal interface IAccountsAccessChecker
{
    void CheckUserAccess( User? user, int stadiumGroupId );
    void CheckRoleAccess( Role? role, int stadiumGroupId );
}
using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Services.Checkers;

internal interface IAccountsAccessChecker
{
    void CheckUserAccess(User? user, int legalId);
    void CheckRoleAccess(Role? role, int legalId);
}
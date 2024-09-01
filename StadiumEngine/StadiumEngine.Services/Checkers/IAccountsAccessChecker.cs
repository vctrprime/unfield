using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Services.Checkers;

internal interface IAccountsAccessChecker
{
    void CheckUserAccess( User? user, int stadiumGroupId );
    void CheckRoleAccess( Role? role, int stadiumGroupId );
}
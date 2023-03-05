using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Services.Facades.Accounts;
using StadiumEngine.Services.Facades.Services.Accounts;

namespace StadiumEngine.Services.Facades.Accounts;

internal class UserQueryFacade : IUserQueryFacade
{
    private readonly IUserRepositoryFacade _userRepositoryFacade;

    public UserQueryFacade(
        IUserRepositoryFacade userRepositoryFacade
    )
    {
        _userRepositoryFacade = userRepositoryFacade;
    }


    public async Task<User?> GetUser( int userId ) => await _userRepositoryFacade.GetUser( userId );

    public async Task<List<User>> GetUsersByLegalId( int legalId ) => await _userRepositoryFacade.GetUsers( legalId );

    public async Task<List<Permission>> GetUserPermissions( int userId )
    {
        User? user = await _userRepositoryFacade.GetUser( userId );

        return user switch
        {
            { Role: { } } => await _userRepositoryFacade.GetPermissions( user.Role.Id ),
            _ => await _userRepositoryFacade.GetPermissions()
        };
    }

    public async Task<List<Stadium>> GetUserStadiums( int userId, int legalId )
    {
        User? user = await _userRepositoryFacade.GetUser( userId );

        return user switch
        {
            { Role: { } } => await _userRepositoryFacade.GetStadiumsForRole( user.Role.Id ),
            _ => await _userRepositoryFacade.GetStadiumsForLegal( legalId )
        };
    }
}
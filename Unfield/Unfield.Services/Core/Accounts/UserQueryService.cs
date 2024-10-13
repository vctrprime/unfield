using Unfield.Domain.Entities.Accounts;
using Unfield.Domain.Repositories.Accounts;
using Unfield.Domain.Services.Core.Accounts;
using Unfield.Services.Facades.Accounts;

namespace Unfield.Services.Core.Accounts;

internal class UserQueryService : IUserQueryService
{
    private readonly IUserRepositoryFacade _userRepositoryFacade;
    private readonly IStadiumRepository _stadiumRepository;

    public UserQueryService( IUserRepositoryFacade userRepositoryFacade, IStadiumRepository stadiumRepository )
    {
        _userRepositoryFacade = userRepositoryFacade;
        _stadiumRepository = stadiumRepository;
    }


    public async Task<User?> GetUserAsync( int userId ) => await _userRepositoryFacade.GetUserAsync( userId );

    public async Task<List<User>> GetUsersByStadiumGroupIdAsync( int stadiumGroupId ) => await _userRepositoryFacade.GetUsersAsync( stadiumGroupId );

    public async Task<List<Permission>> GetUserPermissionsAsync( int userId )
    {
        User? user = await _userRepositoryFacade.GetUserAsync( userId );

        return user switch
        {
            { Role: { } } => await _userRepositoryFacade.GetPermissionsAsync( user.Role.Id ),
            _ => await _userRepositoryFacade.GetPermissionsAsync()
        };
    }

    public async Task<List<Stadium>> GetUserStadiumsAsync( int userId, int stadiumGroupId )
    {
        User? user = await _userRepositoryFacade.GetUserAsync( userId );

        if ( user == null || user.IsSuperuser )
        {
            return await _userRepositoryFacade.GetStadiumsForStadiumGroupAsync( stadiumGroupId );
        }

        return await _userRepositoryFacade.GetStadiumsForUserAsync( user.Id );
    }
    
    public async Task<Dictionary<Stadium, bool>> GetStadiumsForUserAsync( int userId, int stadiumGroupId )
    {
        User? user = await _userRepositoryFacade.GetUserAsync( userId );
        
        List<Stadium> stadiums = await _stadiumRepository.GetForStadiumGroupAsync( stadiumGroupId );

        return stadiums.ToDictionary(
            stadium => stadium,
            stadium => user!.UserStadiums.FirstOrDefault( rs => rs.UserId == userId && stadium.Id == rs.StadiumId ) !=
                       null );
    }
}
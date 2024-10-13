using Unfield.Domain.Entities.Accounts;
using Unfield.Domain.Repositories.Accounts;
using Unfield.Services.Facades.Accounts;

namespace Unfield.Services.Facades.Accounts;

internal class UserRepositoryFacade : IUserRepositoryFacade
{
    private readonly IStadiumGroupRepository _stadiumGroupRepository;
    private readonly IRoleRepositoryFacade _roleRepositoryFacade;
    private readonly IStadiumRepository _stadiumRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUserStadiumRepository _userStadiumRepository;

    public UserRepositoryFacade(
        IStadiumGroupRepository stadiumGroupRepository,
        IRoleRepositoryFacade roleRepositoryFacade,
        IStadiumRepository stadiumRepository,
        IUserRepository userRepository,
        IUserStadiumRepository userStadiumRepository )
    {
        _stadiumGroupRepository = stadiumGroupRepository;
        _roleRepositoryFacade = roleRepositoryFacade;
        _stadiumRepository = stadiumRepository;
        _userRepository = userRepository;
        _userStadiumRepository = userStadiumRepository;
    }

    public async Task<User?> GetUserAsync( string login ) => await _userRepository.GetAsync( login );

    public async Task<User?> GetUserAsync( int userId ) => await _userRepository.GetAsync( userId );

    public async Task<List<User>> GetUsersAsync( int stadiumGroupId ) => await _userRepository.GetAllAsync( stadiumGroupId );

    public void AddUser( User user ) => _userRepository.Add( user );

    public void UpdateUser( User user ) => _userRepository.Update( user );

    public void RemoveUser( User user ) => _userRepository.Remove( user );

    public async Task<List<StadiumGroup>> GetStadiumGroupsAsync( string searchString ) =>
        await _stadiumGroupRepository.GetByFilterAsync( searchString );

    public async Task<Role?> GetRoleAsync( int roleId ) => await _roleRepositoryFacade.GetRoleAsync( roleId );

    public async Task<List<Permission>> GetPermissionsAsync() => await _roleRepositoryFacade.GetPermissionsAsync();

    public async Task<List<Permission>> GetPermissionsAsync( int roleId ) =>
        await _roleRepositoryFacade.GetPermissionsAsync( roleId );

    public async Task<List<Stadium>> GetStadiumsForStadiumGroupAsync( int stadiumGroupId ) =>
        await _stadiumRepository.GetForStadiumGroupAsync( stadiumGroupId );

    public async Task<List<Stadium>> GetStadiumsForUserAsync( int userId ) =>
        await _stadiumRepository.GetForUserAsync( userId );

    public async Task<UserStadium?> GetUserStadiumAsync( int userId, int stadiumId ) =>
        await _userStadiumRepository.GetAsync( userId, stadiumId );

    public void AddUserStadium( UserStadium userStadium ) => _userStadiumRepository.Add( userStadium );

    public void RemoveUserStadium( UserStadium userStadium ) => _userStadiumRepository.Remove( userStadium );
}
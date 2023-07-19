using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Repositories.Accounts;
using StadiumEngine.Services.Facades.Accounts;

namespace StadiumEngine.Services.Facades.Accounts;

internal class UserRepositoryFacade : IUserRepositoryFacade
{
    private readonly ILegalRepository _legalRepository;
    private readonly IRoleRepositoryFacade _roleRepositoryFacade;
    private readonly IStadiumRepository _stadiumRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUserStadiumRepository _userStadiumRepository;

    public UserRepositoryFacade(
        ILegalRepository legalRepository,
        IRoleRepositoryFacade roleRepositoryFacade,
        IStadiumRepository stadiumRepository,
        IUserRepository userRepository,
        IUserStadiumRepository userStadiumRepository )
    {
        _legalRepository = legalRepository;
        _roleRepositoryFacade = roleRepositoryFacade;
        _stadiumRepository = stadiumRepository;
        _userRepository = userRepository;
        _userStadiumRepository = userStadiumRepository;
    }

    public async Task<User?> GetUserAsync( string login ) => await _userRepository.GetAsync( login );

    public async Task<User?> GetUserAsync( int userId ) => await _userRepository.GetAsync( userId );

    public async Task<List<User>> GetUsersAsync( int legalId ) => await _userRepository.GetAllAsync( legalId );

    public void AddUser( User user ) => _userRepository.Add( user );

    public void UpdateUser( User user ) => _userRepository.Update( user );

    public void RemoveUser( User user ) => _userRepository.Remove( user );

    public async Task<List<Legal>> GetLegalsAsync( string searchString ) =>
        await _legalRepository.GetByFilterAsync( searchString );

    public async Task<Role?> GetRoleAsync( int roleId ) => await _roleRepositoryFacade.GetRoleAsync( roleId );

    public async Task<List<Permission>> GetPermissionsAsync() => await _roleRepositoryFacade.GetPermissionsAsync();

    public async Task<List<Permission>> GetPermissionsAsync( int roleId ) =>
        await _roleRepositoryFacade.GetPermissionsAsync( roleId );

    public async Task<List<Stadium>> GetStadiumsForLegalAsync( int legalId ) =>
        await _stadiumRepository.GetForLegalAsync( legalId );

    public async Task<List<Stadium>> GetStadiumsForUserAsync( int userId ) =>
        await _stadiumRepository.GetForUserAsync( userId );

    public async Task<UserStadium?> GetUserStadiumAsync( int userId, int stadiumId ) =>
        await _userStadiumRepository.GetAsync( userId, stadiumId );

    public void AddUserStadium( UserStadium userStadium ) => _userStadiumRepository.Add( userStadium );

    public void RemoveUserStadium( UserStadium userStadium ) => _userStadiumRepository.Remove( userStadium );
}
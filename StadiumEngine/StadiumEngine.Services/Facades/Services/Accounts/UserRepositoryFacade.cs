using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Repositories.Accounts;

namespace StadiumEngine.Services.Facades.Services.Accounts;

internal class UserRepositoryFacade : IUserRepositoryFacade
{
    private readonly ILegalRepository _legalRepository;
    private readonly IRoleRepositoryFacade _roleRepositoryFacade;
    private readonly IStadiumRepository _stadiumRepository;
    private readonly IUserRepository _userRepository;

    public UserRepositoryFacade(
        IUserRepository userRepository,
        ILegalRepository legalRepository,
        IStadiumRepository stadiumRepository,
        IRoleRepositoryFacade roleRepositoryFacade )
    {
        _userRepository = userRepository;
        _legalRepository = legalRepository;
        _stadiumRepository = stadiumRepository;
        _roleRepositoryFacade = roleRepositoryFacade;
    }

    public async Task<User?> GetUser( string login )
    {
        return await _userRepository.Get( login );
    }

    public async Task<User?> GetUser( int userId )
    {
        return await _userRepository.Get( userId );
    }

    public async Task<List<User>> GetUsers( int legalId )
    {
        return await _userRepository.GetAll( legalId );
    }

    public void AddUser( User user )
    {
        _userRepository.Add( user );
    }

    public void UpdateUser( User user )
    {
        _userRepository.Update( user );
    }

    public void RemoveUser( User user )
    {
        _userRepository.Remove( user );
    }

    public async Task<List<Legal>> GetLegals( string searchString )
    {
        return await _legalRepository.GetByFilter( searchString );
    }

    public async Task<Role?> GetRole( int roleId )
    {
        return await _roleRepositoryFacade.GetRole( roleId );
    }

    public async Task<List<Permission>> GetPermissions()
    {
        return await _roleRepositoryFacade.GetPermissions();
    }

    public async Task<List<Permission>> GetPermissions( int roleId )
    {
        return await _roleRepositoryFacade.GetPermissions( roleId );
    }

    public async Task<List<Stadium>> GetStadiumsForLegal( int legalId )
    {
        return await _stadiumRepository.GetForLegal( legalId );
    }

    public async Task<List<Stadium>> GetStadiumsForRole( int roleId )
    {
        return await _stadiumRepository.GetForRole( roleId );
    }
}
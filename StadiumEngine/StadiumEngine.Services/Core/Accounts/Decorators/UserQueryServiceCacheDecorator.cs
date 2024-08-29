using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Services.Core.Accounts;
using StadiumEngine.Domain.Services.Infrastructure;

namespace StadiumEngine.Services.Core.Accounts.Decorators;

internal class UserQueryServiceCacheDecorator : IUserQueryService
{
    private readonly IUserQueryService _userQueryService;
    private readonly ICacheProvider _cacheProvider;

    public UserQueryServiceCacheDecorator( IUserQueryService userQueryService, ICacheProvider cacheProvider )
    {
        _userQueryService = userQueryService;
        _cacheProvider = cacheProvider;
    }

    public async Task<User?> GetUserAsync( int userId ) => 
        await _userQueryService.GetUserAsync( userId );

    public async Task<List<User>> GetUsersByLegalIdAsync( int legalId ) => 
        await _userQueryService.GetUsersByLegalIdAsync( legalId );

    public async Task<List<Permission>> GetUserPermissionsAsync( int userId )
    {
        List<Permission>? result = await _cacheProvider.GetOrCreateAsync( 
            $"{userId}-permissions", 
            3, 
            () => _userQueryService.GetUserPermissionsAsync( userId ) );
        
        return result ?? new List<Permission>();
    }
    
    public async Task<List<Stadium>> GetUserStadiumsAsync( int userId, int legalId ) =>
        await _userQueryService.GetUserStadiumsAsync( userId, legalId );
    
    public async Task<Dictionary<Stadium, bool>> GetStadiumsForUserAsync( int userId, int legalId ) =>
        await _userQueryService.GetStadiumsForUserAsync( userId, legalId );
}
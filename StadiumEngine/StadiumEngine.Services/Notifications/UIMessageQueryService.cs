using StadiumEngine.Common;
using StadiumEngine.Common.Constant;
using StadiumEngine.Common.Enums.Notifications;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Entities.Notifications;
using StadiumEngine.Domain.Repositories.Accounts;
using StadiumEngine.Domain.Repositories.Notifications;
using StadiumEngine.Domain.Services.Core.Notifications;

namespace StadiumEngine.Services.Notifications;

internal class UIMessageQueryService : IUIMessageQueryService
{
    private readonly IUIMessageRepository _uiMessageRepository;
    private readonly IPermissionRepository _permissionRepository;
    private readonly IUserRepository _userRepository;

    public UIMessageQueryService( IUIMessageRepository uiMessageRepository, IPermissionRepository permissionRepository, IUserRepository userRepository )
    {
        _uiMessageRepository = uiMessageRepository;
        _permissionRepository = permissionRepository;
        _userRepository = userRepository;
    }

    public async Task<List<UIMessage>> GetByStadiumIdAsync( int stadiumId, int userId )
    {
        List<UIMessage> messages = await _uiMessageRepository.GetAsync( stadiumId );

        User? user = await _userRepository.GetAsync( userId );

        if ( user == null )
        {
            throw new DomainException( ErrorsKeys.UserNotFound );
        }

        if ( user.RoleId.HasValue )
        {
            List<Permission> permissions = await _permissionRepository.GetForRoleAsync( user.RoleId.Value );
            //только для тех у кого есть права смотреть брони
            if ( permissions.SingleOrDefault( x => x.Name == PermissionsKeys.GetBookings ) == null )
            {
                messages = messages.Where( x => x.MessageType != UIMessageType.BookingFromForm ).ToList();
            }
        }
        
        return messages;
    }
}
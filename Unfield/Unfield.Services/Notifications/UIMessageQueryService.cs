using Unfield.Common;
using Unfield.Common.Constant;
using Unfield.Common.Enums.Notifications;
using Unfield.Common.Exceptions;
using Unfield.Domain.Entities.Accounts;
using Unfield.Domain.Entities.Notifications;
using Unfield.Domain.Repositories.Accounts;
using Unfield.Domain.Repositories.Notifications;
using Unfield.Domain.Services.Core.Notifications;

namespace Unfield.Services.Notifications;

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
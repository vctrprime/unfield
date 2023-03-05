using System.Security.Claims;
using Newtonsoft.Json;

namespace StadiumEngine.DTO.Accounts.Users;

/// <summary>
///     ДТО пользователя при авторизации
/// </summary>
public class AuthorizeUserDto : AuthorizedUserDto
{
    /// <summary>
    ///     Клаймсы пользователя
    /// </summary>
    [JsonIgnore]
    public List<Claim> Claims { get; set; } = null!;
}
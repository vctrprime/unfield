using System.Security.Claims;
using Newtonsoft.Json;

namespace StadiumEngine.DTO.Accounts.Users;

/// <summary>
/// ДТО пользователя при авторизации
/// </summary>
public class AuthorizeUserDto : AuthorizedUserDto
{
    [JsonIgnore]
    public List<Claim> Claims { get; set; }
}

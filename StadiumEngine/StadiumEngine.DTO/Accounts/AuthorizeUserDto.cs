using System.Security.Claims;
using Newtonsoft.Json;

namespace StadiumEngine.DTO.Accounts;

/// <summary>
/// ДТО авторизованного пользователя
/// </summary>
public class AuthorizeUserDto
{
    /// <summary>
    /// Полное имя
    /// </summary>
    public string FullName { get; set; }
    
    [JsonIgnore]
    public List<Claim> Claims { get; set; }
}


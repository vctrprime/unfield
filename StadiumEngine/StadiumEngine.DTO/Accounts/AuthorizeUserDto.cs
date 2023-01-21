using System.Security.Claims;
using Newtonsoft.Json;

namespace StadiumEngine.DTO.Accounts;

/// <summary>
/// ДТО пользователя при авторизации
/// </summary>
public class AuthorizeUserDto
{
    /// <summary>
    /// Полное имя
    /// </summary>
    public string FullName { get; set; }
    
    /// <summary>
    /// Название роли
    /// </summary>
    public string? RoleName { get; set; }
    
    /// <summary>
    /// Флаг суперюзера
    /// </summary>
    public bool IsSuperuser { get; set; }
    
    /// <summary>
    /// Язык
    /// </summary>
    public string Language { get; set; }
    
    [JsonIgnore]
    public List<Claim> Claims { get; set; }
}


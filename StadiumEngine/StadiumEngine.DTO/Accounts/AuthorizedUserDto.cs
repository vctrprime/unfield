namespace StadiumEngine.DTO.Accounts;

/// <summary>
/// ДТО авторизованного пользователя
/// </summary>
public class AuthorizedUserDto
{
    /// <summary>
    /// Полное имя
    /// </summary>
    public string FullName { get; set; }
    
    /// <summary>
    /// Название роли
    /// </summary>
    public string RoleName { get; set; }
}
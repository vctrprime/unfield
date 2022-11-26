namespace StadiumEngine.DTO.Auth;

/// <summary>
/// ДТО данных для авторизации
/// </summary>
public class LoginPostDto
{
    /// <summary>
    /// Имя
    /// </summary>
    public string Username { get; set; }
    
    /// <summary>
    /// Пароль
    /// </summary>
    public string Password { get; set; }
}
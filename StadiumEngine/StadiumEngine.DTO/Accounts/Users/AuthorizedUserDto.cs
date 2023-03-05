namespace StadiumEngine.DTO.Accounts.Users;

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

    /// <summary>
    /// Язык
    /// </summary>
    public string Language { get; set; }

    /// <summary>
    /// Флаг суперюзера
    /// </summary>
    public bool IsSuperuser { get; set; }

    /// <summary>
    /// Флаг админа
    /// </summary>
    public bool IsAdmin { get; set; }

    /// <summary>
    /// Токен для отслеживания изменений
    /// </summary>
    public Guid UniqueToken { get; set; }

    /// <summary>
    /// Организация
    /// </summary>
    public string LegalName { get; set; }
}
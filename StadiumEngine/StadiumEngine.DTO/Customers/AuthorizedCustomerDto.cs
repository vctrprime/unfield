using StadiumEngine.DTO.Accounts.Stadiums;

namespace StadiumEngine.DTO.Customers;

/// <summary>
///     ДТО авторизованного заказчика
/// </summary>
public class AuthorizedCustomerDto
{
    /// <summary>
    ///     Имя
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    ///     Фамилия
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    ///     Язык
    /// </summary>
    public string Language { get; set; } = null!;
    
    /// <summary>
    ///     Язык
    /// </summary>
    public string PhoneNumber { get; set; } = null!;
    
    /// <summary>
    /// Список доступнхы стадионов
    /// </summary>
    public List<StadiumDto> Stadiums { get; set; }
}
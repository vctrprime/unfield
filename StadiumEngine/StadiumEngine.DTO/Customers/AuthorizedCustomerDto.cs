using StadiumEngine.DTO.Accounts.Stadiums;

namespace StadiumEngine.DTO.Customers;

/// <summary>
///     ДТО авторизованного заказчика
/// </summary>
public class AuthorizedCustomerDto : AuthorizedCustomerBaseDto
{
    /// <summary>
    /// Список доступнхы стадионов
    /// </summary>
    public List<StadiumDto>? Stadiums { get; set; }
}
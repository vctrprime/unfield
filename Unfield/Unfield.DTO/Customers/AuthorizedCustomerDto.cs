using Unfield.DTO.Accounts.Stadiums;

namespace Unfield.DTO.Customers;

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
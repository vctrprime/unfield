using System.Security.Claims;
using Newtonsoft.Json;
using StadiumEngine.DTO.Accounts.Stadiums;

namespace StadiumEngine.DTO.Customers;

/// <summary>
///     ДТО заказчика после авторизации
/// </summary>
public class AuthorizeCustomerDto : AuthorizedCustomerBaseDto
{
    /// <summary>
    ///     Клаймсы пользователя
    /// </summary>
    [JsonIgnore]
    public List<Claim> Claims { get; set; } = null!;
    
    /// <summary>
    /// Информация по брони при авторизации через редирект
    /// </summary>
    public AuthorizeCustomerBookingDto? Booking {get; set;} 

}

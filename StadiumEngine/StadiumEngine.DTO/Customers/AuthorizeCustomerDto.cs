using System.Security.Claims;
using Newtonsoft.Json;

namespace StadiumEngine.DTO.Customers;

/// <summary>
///     ДТО заказчика после авторизации
/// </summary>
public class AuthorizeCustomerDto : AuthorizedCustomerDto
{
    /// <summary>
    ///     Клаймсы пользователя
    /// </summary>
    [JsonIgnore]
    public List<Claim> Claims { get; set; } = null!;
    
    /// <summary>
    ///     Номер брони, куда средиректить
    /// </summary>
    public string? BookingNumber { get; set; }
}
namespace StadiumEngine.DTO.Customers;

/// <summary>
/// Информация по брони при авторизации через редирект
/// </summary>
public class AuthorizeCustomerBookingDto
{
    /// <summary>
    ///     Номер брони
    /// </summary>
    public string? Number { get; set; }
    
    /// <summary>
    ///     ID стадиона
    /// </summary>
    public int StadiumId { get; set; }
}
namespace StadiumEngine.DTO.Customers;

/// <summary>
///     ДТО авторизованного заказчика, базовые поля
/// </summary>
public class AuthorizedCustomerBaseDto
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
    /// Отображаемое имя
    /// </summary>
    public string? DisplayName
    {
        get
        {
            if ( String.IsNullOrEmpty( FirstName ) && String.IsNullOrEmpty( LastName ) )
            {
                return null;
            }

            if ( String.IsNullOrEmpty( FirstName ) )
            {
                return LastName;
            }
            
            if ( String.IsNullOrEmpty( LastName ) )
            {
                return FirstName;
            }
            
            return $"{FirstName} {LastName}";
        }
    }
}
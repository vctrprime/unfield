namespace Unfield.DTO;

/// <summary>
/// Информация о среде выполнения
/// </summary>
public class EnvDataDto
{
    /// <summary>
    /// Базовый url экстранета
    /// </summary>
    public string ExtranetHost { get; set; } = null!;
    
    /// <summary>
    /// Базовый url портала
    /// </summary>
    public string PortalHost { get; set; } = null!;

    /// <summary>
    /// Базовый url личного кабинета заказчика
    /// </summary>
    public string CustomerAccountHost { get; set; } = null!;
}
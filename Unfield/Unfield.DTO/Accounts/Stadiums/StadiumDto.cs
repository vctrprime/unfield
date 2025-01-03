using Newtonsoft.Json;

namespace Unfield.DTO.Accounts.Stadiums;

/// <summary>
///     ДТО стадиона
/// </summary>
public class StadiumDto
{
    /// <summary>
    ///     Id
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    ///     Название
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    ///     Адрес
    /// </summary>
    public string Address { get; set; } = null!;

    /// <summary>
    ///     Описание
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    ///     Страна
    /// </summary>
    public string Country { get; set; } = null!;

    /// <summary>
    ///     Регион
    /// </summary>
    public string Region { get; set; } = null!;

    /// <summary>
    ///     Город
    /// </summary>
    public string City { get; set; } = null!;

    /// <summary>
    ///     Дата добавления
    /// </summary>
    public DateTime DateCreated { get; set; }

    /// <summary>
    ///     Связан с пользователем, для которого был запрос
    /// </summary>
    public bool IsUserBound { get; set; }

    /// <summary>
    ///  Токен
    /// </summary>
    public string Token { get; set; } = null!;
    
    /// <summary>
    /// Хост формы бронирования для стадиона
    /// </summary>
    public string? BookingFormHost { get; set; }
}
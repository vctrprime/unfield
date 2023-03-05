namespace StadiumEngine.DTO.Admin;

/// <summary>
///     ДТО организации
/// </summary>
public class LegalDto
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
    ///     ИНН
    /// </summary>
    public string Inn { get; set; } = null!;

    /// <summary>
    ///     Руководитель
    /// </summary>
    public string HeadName { get; set; } = null!;

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
    ///     Кол-во пользователей
    /// </summary>
    public int UsersCount { get; set; }

    /// <summary>
    ///     Кол-во объектов
    /// </summary>
    public int StadiumsCount { get; set; }
}
namespace StadiumEngine.DTO.Admin;

/// <summary>
/// ДТО организации
/// </summary>
public class LegalDto
{
    /// <summary>
    /// Id
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Название
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// ИНН
    /// </summary>
    public string Inn { get; set; }

    /// <summary>
    /// Руководитель
    /// </summary>
    public string HeadName { get; set; }

    /// <summary>
    /// Описание
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Страна
    /// </summary>
    public string Country { get; set; }

    /// <summary>
    /// Регион
    /// </summary>
    public string Region { get; set; }

    /// <summary>
    /// Город
    /// </summary>
    public string City { get; set; }

    /// <summary>
    /// Дата добавления
    /// </summary>
    public DateTime DateCreated { get; set; }

    /// <summary>
    /// Кол-во пользователей
    /// </summary>
    public int UsersCount { get; set; }

    /// <summary>
    /// Кол-во объектов
    /// </summary>
    public int StadiumsCount { get; set; }
}
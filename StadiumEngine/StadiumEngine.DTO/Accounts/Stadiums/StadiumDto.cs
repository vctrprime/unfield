namespace StadiumEngine.DTO.Accounts.Stadiums;

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
    public string Name { get; set; }

    /// <summary>
    ///     Адрес
    /// </summary>
    public string Address { get; set; }

    /// <summary>
    ///     Описание
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    ///     Страна
    /// </summary>
    public string Country { get; set; }

    /// <summary>
    ///     Регион
    /// </summary>
    public string Region { get; set; }

    /// <summary>
    ///     Город
    /// </summary>
    public string City { get; set; }

    /// <summary>
    ///     Дата добавления
    /// </summary>
    public DateTime DateCreated { get; set; }

    /// <summary>
    ///     Связан с ролью, для которой был запрос
    /// </summary>
    public bool IsRoleBound { get; set; }
}
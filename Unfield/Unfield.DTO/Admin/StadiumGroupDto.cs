namespace Unfield.DTO.Admin;

/// <summary>
///     ДТО организации
/// </summary>
public class StadiumGroupDto
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
    ///     Описание
    /// </summary>
    public string? Description { get; set; }
    

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
namespace StadiumEngine.DTO.Utils;

/// <summary>
///     ДТО добавленного легала
/// </summary>
public class AddLegalDto
{
    /// <summary>
    ///     Id
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    ///     ИНН
    /// </summary>
    public string Inn { get; set; } = null!;

    /// <summary>
    ///     Руководитель
    /// </summary>
    public string HeadName { get; set; } = null!;

    /// <summary>
    ///     Название
    /// </summary>
    public string Name { get; set; } = null!;
}
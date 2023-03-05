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
    public string Inn { get; set; }

    /// <summary>
    ///     Руководитель
    /// </summary>
    public string HeadName { get; set; }

    /// <summary>
    ///     Название
    /// </summary>
    public string Name { get; set; }
}
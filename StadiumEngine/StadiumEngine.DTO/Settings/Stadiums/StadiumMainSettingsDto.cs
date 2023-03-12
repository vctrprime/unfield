namespace StadiumEngine.DTO.Settings.Stadiums;

/// <summary>
/// ДТО основных настроек
/// </summary>
public class StadiumMainSettingsDto
{
    /// <summary>
    /// Отображаемое имя
    /// </summary>
    public string? Name { get; set; }
    
    /// <summary>
    /// Отображаемое описание
    /// </summary>
    public string? Description { get; set; }
    
    /// <summary>
    /// Времяя октрытия
    /// </summary>
    public int OpenTime { get; set; }
    
    /// <summary>
    /// Время закрытия
    /// </summary>
    public int CloseTime { get; set; }
}
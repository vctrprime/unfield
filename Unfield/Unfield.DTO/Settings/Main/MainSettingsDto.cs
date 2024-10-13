namespace Unfield.DTO.Settings.Main;

/// <summary>
/// ДТО основных настроек
/// </summary>
public class MainSettingsDto
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
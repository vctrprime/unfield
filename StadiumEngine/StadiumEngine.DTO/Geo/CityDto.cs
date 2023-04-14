namespace StadiumEngine.DTO.Geo;

/// <summary>
/// Дто города
/// </summary>
public class CityDto
{
    /// <summary>
    /// Id
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Название
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Краткое название
    /// </summary>
    public string? ShortName { get; set; }
    
    /// <summary>
    /// Название региона
    /// </summary>
    public string? RegionName { get; set; }
    
    /// <summary>
    /// Краткое название региона
    /// </summary>
    public string? RegionShortName { get; set; }
    
    /// <summary>
    /// Название страны
    /// </summary>
    public string? CountryName { get; set; }
    
    /// <summary>
    /// Краткое нзвание страны
    /// </summary>
    public string? CountryShortName { get; set; }

    /// <summary>
    /// Отображаемое имя
    /// </summary>
    public string DisplayName => $"{ShortName ?? Name}, {RegionShortName ?? RegionName}";
}
using Newtonsoft.Json;
using Unfield.DTO.Offers.Fields;

namespace Unfield.DTO.Schedule;

/// <summary>
/// Площадки для расписания
/// </summary>
public class SchedulerFieldsDto
{
    /// <summary>
    /// Час открытия
    /// </summary>
    public int StartHour { get; set; }
    
    /// <summary>
    /// Час закрытия
    /// </summary>
    public int EndHour { get; set; }

    /// <summary>
    /// Площадки
    /// </summary>
    public List<SchedulerFieldDto> Data { get; set; } = new();
}


/// <summary>
/// Площадка для расписания
/// </summary>
public class SchedulerFieldDto
{
    /// <summary>
    /// Id
    /// </summary>
    [JsonProperty( "field_id" )]
    public int Id => Data.Id;

    /// <summary>
    /// Название
    /// </summary>
    public string Name => Data.Name;

    /// <summary>
    /// Данные о площадке
    /// </summary>
    public FieldDto Data { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    public SchedulerFieldDto( FieldDto data )
    {
        Data = data;
    }
}
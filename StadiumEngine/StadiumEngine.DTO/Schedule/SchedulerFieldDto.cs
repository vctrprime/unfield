using Newtonsoft.Json;
using StadiumEngine.DTO.Offers.Fields;

namespace StadiumEngine.DTO.Schedule;

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
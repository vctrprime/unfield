using Newtonsoft.Json;

namespace Unfield.DTO.Schedule;

/// <summary>
/// Ивент для расписания
/// </summary>
public class SchedulerEventDto
{
    /// <summary>
    /// Id
    /// </summary>
    [JsonProperty( "event_id" )]
    public int EventId { get; set; }

    /// <summary>
    /// Id поля
    /// </summary>
    [JsonProperty( "field_id" )]
    public int FieldId { get; set; }

    /// <summary>
    /// Название
    /// </summary>
    public string Title { get; set; } = null!;

    /// <summary>
    /// Дата/время начала
    /// </summary>
    public DateTime Start { get; set; }

    /// <summary>
    /// Дата/время окончания
    /// </summary>
    public DateTime End { get; set; }

    /// <summary>
    /// Выключено
    /// </summary>
    public bool Disabled { get; set; }

    /// <summary>
    /// Цвет
    /// </summary>
    public string? Color { get; set; }

    /// <summary>
    /// Редактируемое
    /// </summary>
    public bool Editable { get; set; }

    /// <summary>
    /// Удаляемое
    /// </summary>
    public bool Deletable { get; set; }

    /// <summary>
    /// Перетаскиваемое
    /// </summary>
    public bool Draggable { get; set; }

    /// <summary>
    /// На весь день
    /// </summary>
    public bool AllDay { get; set; }

    /// <summary>
    /// Данные о бронировании
    /// </summary>
    public BookingDto? Data { get; set; }
    
}
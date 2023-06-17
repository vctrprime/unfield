using Newtonsoft.Json;
using StadiumEngine.Common.Enums.BookingForm;

namespace StadiumEngine.DTO.Schedule;

/// <summary>
/// Ивент для расписания
/// </summary>
public class SchedulerEventDto
{
    /// <summary>
    /// Id
    /// </summary>
    [JsonProperty( "event_id" )]
    public int EventId { get; }

    /// <summary>
    /// Id поля
    /// </summary>
    [JsonProperty( "field_id" )]
    public int FieldId { get; }

    /// <summary>
    /// Название
    /// </summary>
    public string Title { get; }

    /// <summary>
    /// Дата/время начала
    /// </summary>
    public DateTime Start { get; }

    /// <summary>
    /// Дата/время окончания
    /// </summary>
    public DateTime End { get; }

    /// <summary>
    /// Выключено
    /// </summary>
    public bool Disabled => Data == null;

    /// <summary>
    /// Цвет
    /// </summary>
    public string? Color { get; }

    /// <summary>
    /// Редактируемое
    /// </summary>
    public bool Editable { get; }

    /// <summary>
    /// Удаляемое
    /// </summary>
    public bool Deletable { get; }

    /// <summary>
    /// Перетаскиваемое
    /// </summary>
    public bool Draggable => false;

    /// <summary>
    /// На весь день
    /// </summary>
    public bool AllDay => false;

    /// <summary>
    /// Данные о бронировании
    /// </summary>
    public BookingDto? Data { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    /// <param name="withData"></param>
    public SchedulerEventDto( BookingDto data, bool withData = true )
    {
        EventId = data.Id;
        Start = data.Day.AddHours( ( double )data.StartHour );
        End = Start.AddHours( ( double )data.HoursCount );
        Title = $"{data.Number} | {data.Customer.Name}";
        Color = data.Source == BookingSource.Form ? "green" : "blue";
        Editable = End > DateTime.Now;
        Deletable = End > DateTime.Now;

        if ( withData )
        {
            Data = data;
        }
    }
}
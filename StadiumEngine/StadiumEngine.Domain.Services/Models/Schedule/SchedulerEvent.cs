using System.Globalization;
using StadiumEngine.Common.Enums.Bookings;
using StadiumEngine.Domain.Entities.Bookings;
using StadiumEngine.Domain.Entities.Settings;

namespace StadiumEngine.Domain.Services.Models.Schedule;

public class SchedulerEvent
{
    public int EventId { get; }
    
    public int FieldId { get; }

    public string Title => BookingsCount <= 1 ? _title : 
        Common.Resources.Scheduler.ResourceManager.GetString( "multiple_bookings", new CultureInfo( Language ) ) ?? "Multiple bookings";
    
    private string _title { get; }
    
    public DateTime Start { get; }
    
    public DateTime End { get; set; }
    
    public bool Disabled => Data == null;
    
    public string? Color { get; }
    
    public bool Editable { get; }

    public bool Deletable => false;
    
    public bool Draggable => false;
    
    public bool AllDay => false;
    public Booking? Data { get; }
    
    public int BookingsCount { get; set; }
    private string Language { get; }
    
    public SchedulerEvent( 
        Booking data, 
        string language, 
        int? fieldId = null,
        DateTime? start = null,
        DateTime? end = null)
    {
        Language = language;
        EventId = data.Id;
        Start = start ?? data.Day.AddHours( ( double )data.StartHour );
        End = end ?? Start.AddHours( ( double )data.HoursCount );
        _title = $"{data.Number} | {data.Customer.Name} | {data.TotalAmountAfterDiscount}";
        Color = data.IsWeekly ? "#20B2AA" : data.Source == BookingSource.Form ? "#3CB371" : "#4682B4";
        Editable = true;
        BookingsCount = 1;

        if ( !fieldId.HasValue )
        {
            FieldId = data.Field.Id;
            Data = data;
        }
        else
        {
            FieldId = fieldId.Value;
        }
    }
    
    public SchedulerEvent( BreakField breakField, DateTime date, string language )
    {
        Language = language;
        EventId = 0;
        Start = date.AddHours( ( double )breakField.Break.StartHour );
        End = date.AddHours( ( double ) breakField.Break.EndHour );
        _title = Common.Resources.Scheduler.ResourceManager.GetString( "break", new CultureInfo( Language ) ) ?? "Break";
        Color = "";
        Editable = false;
        FieldId = breakField.FieldId;
    }
}
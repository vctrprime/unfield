using StadiumEngine.Common.Enums.BookingForm;
using StadiumEngine.Domain.Entities.Bookings;
using StadiumEngine.Domain.Entities.Settings;

namespace StadiumEngine.Domain.Services.Models.Schedule;

public class SchedulerEvent
{
    public int EventId { get; }
    
    public int FieldId { get; }
    
    public string Title { get; }
    
    public DateTime Start { get; }
    
    public DateTime End { get; }
    
    public bool Disabled => Data == null;
    
    public string? Color { get; }
    
    public bool Editable { get; }
    
    public bool Deletable { get; }
    
    public bool Draggable => false;
    
    public bool AllDay => false;
    public Booking? Data { get; }
    
    public SchedulerEvent( Booking data, int? fieldId = null )
    {
        EventId = data.Id;
        Start = data.Day.AddHours( ( double )data.StartHour );
        End = Start.AddHours( ( double )data.HoursCount );
        Title = $"{data.Number} | {data.Customer.Name} | {data.Amount}";
        Color = data.IsWeekly ? "#20B2AA" : data.Source == BookingSource.Form ? "#3CB371" : "#4682B4";
        Editable = End > DateTime.Now;
        Deletable = End > DateTime.Now;

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
        EventId = 0;
        Start = date.AddHours( ( double )breakField.Break.StartHour );
        End = date.AddHours( ( double ) breakField.Break.EndHour );
        Title = GetBreakTitle( language );
        Color = "";
        Editable = false;
        Deletable = false;
        FieldId = breakField.FieldId;
    }

    private string GetBreakTitle(string language) =>
        language switch
        {
            "en" => "Break",
            _ => "Перерыв"
        };
}
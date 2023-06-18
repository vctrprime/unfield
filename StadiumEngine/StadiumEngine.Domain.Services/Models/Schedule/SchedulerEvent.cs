using StadiumEngine.Common.Enums.BookingForm;
using StadiumEngine.Domain.Entities.Bookings;

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
        Color = data.Source == BookingSource.Form ? "#3CB371" : "#4682B4";
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
}
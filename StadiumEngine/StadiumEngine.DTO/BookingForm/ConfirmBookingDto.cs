namespace StadiumEngine.DTO.BookingForm;

/// <summary>
/// ДТО после подвтерждения брони
/// </summary>
public class ConfirmBookingDto : BaseEmptySuccessDto
{
    /// <summary>
    /// URL для редиректа на ЛК
    /// </summary>
    public string? RedirectUrl{ get; set; }
}
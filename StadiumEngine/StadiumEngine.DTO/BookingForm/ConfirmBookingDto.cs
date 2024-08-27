namespace StadiumEngine.DTO.BookingForm;

/// <summary>
/// ДТО после подвтерждения брони
/// </summary>
public class ConfirmBookingDto : BaseEmptySuccessDto
{
    /// <summary>
    /// Токен для редиректа на ЛК
    /// </summary>
    public string? RedirectToken { get; set; }
}
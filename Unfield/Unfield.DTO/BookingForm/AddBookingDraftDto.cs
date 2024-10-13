namespace Unfield.DTO.BookingForm;

/// <summary>
/// ДТО после создания черновика брони
/// </summary>
public class AddBookingDraftDto
{
    /// <summary>
    /// Номер брони
    /// </summary>
    public string BookingNumber { get; set; } = null!;
}
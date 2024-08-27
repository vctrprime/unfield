namespace StadiumEngine.Domain.Services.Core.BookingForm.Builders;

public interface ICustomerAccountRedirectUrlBuilder
{
    string? Build( string? token );
}
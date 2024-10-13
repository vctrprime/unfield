using Unfield.Domain.Services.Models.Customers;

namespace Unfield.Domain.Services.Core.Customers;

public interface IConfirmBookingRedirectProcessor
{
    Task<ConfirmBookingRedirectResult> ProcessAsync( string token, string language );
}
using StadiumEngine.Domain.Services.Models.Customers;

namespace StadiumEngine.Domain.Services.Core.Customers;

public interface IConfirmBookingRedirectProcessor
{
    Task<ConfirmBookingRedirectResult> ProcessAsync( string token );
}
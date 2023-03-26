using StadiumEngine.Common;
using StadiumEngine.Domain.Services.Infrastructure;

namespace StadiumEngine.Services.Infrastructure;

internal class SmsSender : ISmsSender
{
    public async Task SendPasswordAsync( string phoneNumber, string password, string language )
    {
        switch ( language )
        {
            case "en":
                await Send( phoneNumber, String.Format( SmsTemplates.SendPasswordEn, password ) );
                break;
            default:
                await Send( phoneNumber, String.Format( SmsTemplates.SendPasswordRu, password ) );
                break;
        }
    }

    private static async Task Send( string phoneNumber, string message ) =>
        await Task.Run( () => Console.WriteLine( message ) );
}
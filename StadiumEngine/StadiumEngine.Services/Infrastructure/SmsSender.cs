using StadiumEngine.Common;
using StadiumEngine.Domain.Services.Infrastructure;

namespace StadiumEngine.Services.Infrastructure;

internal class SmsSender : ISmsSender
{
    public async Task SendPassword(string phoneNumber, string password, string language)
    {
        switch (language)
        {
            case "en":
                await Send(phoneNumber, string.Format(SmsTemplates.SendPasswordEn, password));
                break;
            default:
                await Send(phoneNumber, string.Format(SmsTemplates.SendPasswordRu, password));
                break;
        }
    }
    
    private async Task Send(string phoneNumber, string message)
    {
        Console.WriteLine(message);
    }

}
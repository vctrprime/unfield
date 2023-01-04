using StadiumEngine.Domain.Services.Infrastructure;

namespace StadiumEngine.Services.Infrastructure;

internal class SmsSender : ISmsSender
{
    public async Task Send(string phoneNumber, string message)
    {
        Console.WriteLine(message);
    }
}
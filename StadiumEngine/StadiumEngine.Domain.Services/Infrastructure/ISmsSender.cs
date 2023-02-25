

namespace StadiumEngine.Domain.Services.Infrastructure;

public interface ISmsSender
{
    Task SendPassword(string phoneNumber, string password, string language);
}
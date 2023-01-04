using System.Threading.Tasks;

namespace StadiumEngine.Domain.Services.Infrastructure;

public interface ISmsSender
{
    Task Send(string phoneNumber, string message);
}
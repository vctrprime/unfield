using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Handlers.Facades;

internal interface IAddUserHandlerFacade
{
    Task<string> AddUser(User user);
    Task SendPassword(string phoneNumber, string password, string language);
}
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Services.Facades.Accounts;
using StadiumEngine.Domain.Services.Infrastructure;

namespace StadiumEngine.Handlers.Facades;

internal class AddUserHandlerFacade : IAddUserHandlerFacade
{
    private readonly IUserFacade _userFacade;
    private readonly ISmsSender _smsSender;

    public AddUserHandlerFacade(IUserFacade userFacade, ISmsSender smsSender)
    {
        _userFacade = userFacade;
        _smsSender = smsSender;
    }
    
    public async Task<string> AddUser(User user)
    {
        return await _userFacade.AddUser(user);
    }

    public async Task SendPassword(string phoneNumber, string password, string language)
    {
        await _smsSender.SendPassword(phoneNumber, password, language);
    }
}
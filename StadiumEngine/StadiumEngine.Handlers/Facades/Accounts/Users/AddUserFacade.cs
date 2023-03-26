using StadiumEngine.Domain;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Services.Facades.Accounts;
using StadiumEngine.Domain.Services.Infrastructure;
using StadiumEngine.DTO.Accounts.Users;

namespace StadiumEngine.Handlers.Facades.Accounts.Users;

internal class AddUserFacade : IAddUserFacade
{
    private readonly IUserCommandFacade _commandFacade;
    private readonly ISmsSender _smsSender;

    public AddUserFacade( IUserCommandFacade commandFacade, ISmsSender smsSender )
    {
        _commandFacade = commandFacade;
        _smsSender = smsSender;
    }

    public async Task<AddUserDto> AddAsync( User user, IUnitOfWork unitOfWork )
    {
        string password = await _commandFacade.AddUserAsync( user );
        await unitOfWork.SaveChangesAsync();

        await _smsSender.SendPasswordAsync( user.PhoneNumber, password, user.Language );

        return new AddUserDto();
    }
}
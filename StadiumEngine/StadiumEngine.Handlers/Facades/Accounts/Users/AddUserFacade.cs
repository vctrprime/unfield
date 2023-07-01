using StadiumEngine.Domain;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Services.Core.Accounts;
using StadiumEngine.Domain.Services.Infrastructure;
using StadiumEngine.DTO.Accounts.Users;

namespace StadiumEngine.Handlers.Facades.Accounts.Users;

internal class AddUserFacade : IAddUserFacade
{
    private readonly IUserCommandService _commandService;
    private readonly ISmsSender _smsSender;
    private readonly IUnitOfWork _unitOfWork;

    public AddUserFacade( IUserCommandService commandService, ISmsSender smsSender, IUnitOfWork unitOfWork )
    {
        _commandService = commandService;
        _smsSender = smsSender;
        _unitOfWork = unitOfWork;
    }

    public async Task<AddUserDto> AddAsync( User user )
    {
        string password = await _commandService.AddUserAsync( user );
        await _unitOfWork.SaveChangesAsync();

        await _smsSender.SendPasswordAsync( user.PhoneNumber, password, user.Language );

        return new AddUserDto();
    }
}
using AutoMapper;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Accounts.Users;
using StadiumEngine.Handlers.Commands.Accounts.Users;
using StadiumEngine.Handlers.Facades;

namespace StadiumEngine.Handlers.Handlers.Accounts.Users;

internal sealed class AddUserHandler : BaseCommandHandler<AddUserCommand, AddUserDto>
{
    private readonly IAddUserHandlerFacade _facade;

    public AddUserHandler(
        IAddUserHandlerFacade facade,
        IMapper mapper, 
        IClaimsIdentityService claimsIdentityService, 
        IUnitOfWork unitOfWork) : base(mapper, claimsIdentityService, unitOfWork, false)
    {
        _facade = facade;
    }
    
    protected override async ValueTask<AddUserDto> HandleCommand(AddUserCommand request, CancellationToken cancellationToken)
    {
        var user = Mapper.Map<User>(request);
        user.LegalId = _legalId;
        user.UserCreatedId = _userId;

        var password = await _facade.AddUser(user);
        await UnitOfWork.SaveChanges();
        
        await _facade.SendPassword(user.PhoneNumber, password, user.Language);
        
        return new AddUserDto();
    }
}
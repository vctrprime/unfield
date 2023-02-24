using AutoMapper;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Services;
using StadiumEngine.Domain.Services.Facades.Accounts;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.Domain.Services.Infrastructure;
using StadiumEngine.DTO.Accounts.Users;
using StadiumEngine.Handlers.Commands.Accounts.Users;

namespace StadiumEngine.Handlers.Handlers.Accounts.Users;

internal sealed class AddUserHandler : BaseRequestHandler<AddUserCommand, AddUserDto>
{
    private readonly IUserFacade _userFacade;
    private readonly ISmsSender _smsSender;

    public AddUserHandler(
        IUserFacade userFacade, 
        ISmsSender smsSender,
        IMapper mapper, 
        IClaimsIdentityService claimsIdentityService, 
        IUnitOfWork unitOfWork) : base(mapper, claimsIdentityService, unitOfWork)
    {
        _userFacade = userFacade;
        _smsSender = smsSender;
    }
    
    public override async ValueTask<AddUserDto> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        var user = Mapper.Map<User>(request);
        user.LegalId = _legalId;
        user.UserCreatedId = _userId;

        var password = await _userFacade.AddUser(user);
        await UnitOfWork.SaveChanges();
        
        await _smsSender.SendPassword(user.PhoneNumber, password, user.Language);
        
        return new AddUserDto();
    }
}
using AutoMapper;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Services;
using StadiumEngine.Domain.Services.Facades.Accounts;
using StadiumEngine.DTO.Accounts.Users;
using StadiumEngine.Handlers.Commands.Accounts.Users;

namespace StadiumEngine.Handlers.Handlers.Accounts.Users;

internal sealed class AuthorizeUserHandler : BaseRequestHandler<AuthorizeUserCommand, AuthorizeUserDto?>
{
    private readonly IUserFacade _userFacade;

    public AuthorizeUserHandler(
        IUserFacade userFacade,
        IMapper mapper, 
        IUnitOfWork unitOfWork
    ) 
        : base(mapper, null, unitOfWork)
    {
        _userFacade = userFacade;
    }

    public override async ValueTask<AuthorizeUserDto?> Handle(AuthorizeUserCommand request, CancellationToken cancellationToken)
    {
        if (request.Login == null || request.Password == null) return null;

        var user = await _userFacade.AuthorizeUser(request.Login, request.Password);
        await UnitOfWork.SaveChanges();

        var userDto = Mapper.Map<AuthorizeUserDto>(user);
        
        return userDto;
    }
}
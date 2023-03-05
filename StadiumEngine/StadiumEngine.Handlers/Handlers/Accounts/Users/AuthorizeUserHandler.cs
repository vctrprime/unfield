using AutoMapper;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Services.Facades.Accounts;
using StadiumEngine.DTO.Accounts.Users;
using StadiumEngine.Handlers.Commands.Accounts.Users;

namespace StadiumEngine.Handlers.Handlers.Accounts.Users;

internal sealed class AuthorizeUserHandler : BaseCommandHandler<AuthorizeUserCommand, AuthorizeUserDto?>
{
    private readonly IUserCommandFacade _userFacade;

    public AuthorizeUserHandler(
        IUserCommandFacade userFacade,
        IMapper mapper,
        IUnitOfWork unitOfWork
    )
        : base( mapper, null, unitOfWork )
    {
        _userFacade = userFacade;
    }

    protected override async ValueTask<AuthorizeUserDto?> HandleCommand( AuthorizeUserCommand request,
        CancellationToken cancellationToken )
    {
        if (request.Login == null || request.Password == null) return null;

        var user = await _userFacade.AuthorizeUser( request.Login, request.Password );

        var userDto = Mapper.Map<AuthorizeUserDto>( user );

        return userDto;
    }
}
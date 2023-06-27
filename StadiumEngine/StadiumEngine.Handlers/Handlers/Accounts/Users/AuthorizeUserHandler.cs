using AutoMapper;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Services.Application.Accounts;
using StadiumEngine.DTO.Accounts.Users;
using StadiumEngine.Commands.Accounts.Users;

namespace StadiumEngine.Handlers.Handlers.Accounts.Users;

internal sealed class AuthorizeUserHandler : BaseCommandHandler<AuthorizeUserCommand, AuthorizeUserDto?>
{
    private readonly IUserCommandService _commandService;

    public AuthorizeUserHandler(
        IUserCommandService commandService,
        IMapper mapper,
        IUnitOfWork unitOfWork
    )
        : base( mapper, null, unitOfWork )
    {
        _commandService = commandService;
    }

    protected override async ValueTask<AuthorizeUserDto?> HandleCommandAsync( AuthorizeUserCommand request,
        CancellationToken cancellationToken )
    {
        if ( request.Login == null || request.Password == null )
        {
            return null;
        }

        User user = await _commandService.AuthorizeUserAsync( request.Login, request.Password );

        AuthorizeUserDto? userDto = Mapper.Map<AuthorizeUserDto>( user );

        return userDto;
    }
}
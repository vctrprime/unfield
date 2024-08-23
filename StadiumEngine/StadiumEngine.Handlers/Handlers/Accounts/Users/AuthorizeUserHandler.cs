using AutoMapper;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Services.Core.Accounts;
using StadiumEngine.DTO.Accounts.Users;
using StadiumEngine.Commands.Accounts.Users;

namespace StadiumEngine.Handlers.Handlers.Accounts.Users;

internal sealed class AuthorizeUserHandler : BaseCommandHandler<AuthorizeUserCommand, AuthorizeUserDto?>
{
    private readonly IUserCommandService _commandService;
    private readonly IUserQueryService _queryService;

    public AuthorizeUserHandler(
        IUserCommandService commandService,
        IUserQueryService queryService,
        IMapper mapper,
        IUnitOfWork unitOfWork
    )
        : base( mapper, null, unitOfWork )
    {
        _commandService = commandService;
        _queryService = queryService;
    }

    protected override async ValueTask<AuthorizeUserDto?> HandleCommandAsync( AuthorizeUserCommand request,
        CancellationToken cancellationToken )
    {
        if ( request.Login == null || request.Password == null )
        {
            return null;
        }

        User user = await _commandService.AuthorizeUserAsync( request.Login, request.Password );
        
        List<Permission> permissions = await _queryService.GetUserPermissionsAsync( _userId );

        List<UserPermissionDto> permissionsDto = Mapper.Map<List<UserPermissionDto>>( permissions );

        AuthorizeUserDto? userDto = Mapper.Map<AuthorizeUserDto>( user );
        userDto.Permissions = permissionsDto;

        return userDto;
    }
}
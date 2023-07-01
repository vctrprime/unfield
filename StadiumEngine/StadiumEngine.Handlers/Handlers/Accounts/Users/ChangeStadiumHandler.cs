using System.Security.Claims;
using AutoMapper;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Extensions;
using StadiumEngine.Domain.Services.Core.Accounts;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Accounts.Users;
using StadiumEngine.Commands.Accounts.Users;

namespace StadiumEngine.Handlers.Handlers.Accounts.Users;

internal sealed class ChangeStadiumHandler : BaseCommandHandler<ChangeStadiumCommand, AuthorizeUserDto?>
{
    private readonly IUserCommandService _commandService;

    public ChangeStadiumHandler(
        IUserCommandService commandService,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService,
        IUnitOfWork unitOfWork ) : base(
        mapper,
        claimsIdentityService,
        unitOfWork,
        false )
    {
        _commandService = commandService;
    }

    protected override async ValueTask<AuthorizeUserDto?> HandleCommandAsync( ChangeStadiumCommand request,
        CancellationToken cancellationToken )
    {
        User user = await _commandService.ChangeStadiumAsync( _userId, request.StadiumId );

        AuthorizeUserDto? userDto = Mapper.Map<AuthorizeUserDto>( user );
        Claim? stadiumClaim = userDto.Claims.FirstOrDefault( s => s.Type == "stadiumId" );

        if ( stadiumClaim == null )
        {
            return userDto;
        }

        userDto.Claims.Remove( stadiumClaim );
        userDto.Claims.Add( new Claim( "stadiumId", request.StadiumId.ToString() ) );
        userDto.UniqueToken = user.GetUserToken( request.StadiumId );

        return userDto;
    }
}
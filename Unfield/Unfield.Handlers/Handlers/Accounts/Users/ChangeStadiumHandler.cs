using System.Security.Claims;
using AutoMapper;
using Unfield.Domain;
using Unfield.Domain.Entities.Accounts;
using Unfield.Domain.Extensions;
using Unfield.Domain.Services.Core.Accounts;
using Unfield.Domain.Services.Identity;
using Unfield.DTO.Accounts.Users;
using Unfield.Commands.Accounts.Users;

namespace Unfield.Handlers.Handlers.Accounts.Users;

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
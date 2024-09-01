using AutoMapper;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Accounts.Users;
using StadiumEngine.Commands.Admin;
using StadiumEngine.Handlers.Facades.Accounts.StadiumGroups;

namespace StadiumEngine.Handlers.Handlers.Admin;

internal sealed class ChangeStadiumGroupHandler : BaseCommandHandler<ChangeStadiumGroupCommand, AuthorizeUserDto?>
{
    private readonly IChangeStadiumGroupFacade _facade;

    public ChangeStadiumGroupHandler(
        IChangeStadiumGroupFacade facade,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService,
        IUnitOfWork unitOfWork ) : base(
        mapper,
        claimsIdentityService,
        unitOfWork,
        false )
    {
        _facade = facade;
    }

    protected override async ValueTask<AuthorizeUserDto?> HandleCommandAsync( ChangeStadiumGroupCommand request,
        CancellationToken cancellationToken )
    {
        User? user = await _facade.ChangeAsync( request, _userId );
        AuthorizeUserDto? userDto = Mapper.Map<AuthorizeUserDto>( user );

        return userDto;
    }
}
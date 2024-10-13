using AutoMapper;
using Unfield.Domain;
using Unfield.Domain.Entities.Accounts;
using Unfield.Domain.Services.Identity;
using Unfield.DTO.Accounts.Users;
using Unfield.Commands.Admin;
using Unfield.Handlers.Facades.Accounts.StadiumGroups;

namespace Unfield.Handlers.Handlers.Admin;

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
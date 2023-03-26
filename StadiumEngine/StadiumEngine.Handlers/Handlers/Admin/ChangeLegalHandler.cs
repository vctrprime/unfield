using AutoMapper;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Accounts.Users;
using StadiumEngine.Commands.Admin;
using StadiumEngine.Handlers.Facades.Accounts.Legals;

namespace StadiumEngine.Handlers.Handlers.Admin;

internal sealed class ChangeLegalHandler : BaseCommandHandler<ChangeLegalCommand, AuthorizeUserDto?>
{
    private readonly IChangeLegalFacade _facade;

    public ChangeLegalHandler(
        IChangeLegalFacade facade,
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

    protected override async ValueTask<AuthorizeUserDto?> HandleCommandAsync( ChangeLegalCommand request,
        CancellationToken cancellationToken )
    {
        User? user = await _facade.ChangeAsync( request, _userId );
        AuthorizeUserDto? userDto = Mapper.Map<AuthorizeUserDto>( user );

        return userDto;
    }
}
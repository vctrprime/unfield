using AutoMapper;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Accounts.Users;
using StadiumEngine.Commands.Accounts.Users;
using StadiumEngine.Handlers.Facades.Accounts.Users;

namespace StadiumEngine.Handlers.Handlers.Accounts.Users;

internal sealed class AddUserHandler : BaseCommandHandler<AddUserCommand, AddUserDto>
{
    private readonly IAddUserFacade _facade;

    public AddUserHandler(
        IAddUserFacade facade,
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

    protected override async ValueTask<AddUserDto> HandleCommandAsync( AddUserCommand request,
        CancellationToken cancellationToken )
    {
        User? user = Mapper.Map<User>( request );
        user.LegalId = _legalId;
        user.UserCreatedId = _userId;

        return await _facade.AddAsync( user );
    }
}
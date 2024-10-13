using AutoMapper;
using Unfield.Domain;
using Unfield.Domain.Entities.Accounts;
using Unfield.Domain.Services.Identity;
using Unfield.DTO.Accounts.Users;
using Unfield.Commands.Accounts.Users;
using Unfield.Handlers.Facades.Accounts.Users;

namespace Unfield.Handlers.Handlers.Accounts.Users;

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
        unitOfWork )
    {
        _facade = facade;
    }

    protected override async ValueTask<AddUserDto> HandleCommandAsync( AddUserCommand request,
        CancellationToken cancellationToken )
    {
        User? user = Mapper.Map<User>( request );
        user.StadiumGroupId = _stadiumGroupId;
        user.UserCreatedId = _userId;

        return await _facade.AddAsync( user );
    }
}
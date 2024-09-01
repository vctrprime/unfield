using AutoMapper;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Accounts.Users;
using StadiumEngine.Commands.Accounts.Users;
using StadiumEngine.Handlers.Facades.Accounts.Users;

namespace StadiumEngine.Handlers.Handlers.Accounts.Users;

internal sealed class UpdateUserHandler : BaseCommandHandler<UpdateUserCommand, UpdateUserDto>
{
    private readonly IUpdateUserFacade _facade;

    public UpdateUserHandler(
        IUpdateUserFacade facade,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService,
        IUnitOfWork unitOfWork ) : base( mapper, claimsIdentityService, unitOfWork )
    {
        _facade = facade;
    }

    protected override async ValueTask<UpdateUserDto> HandleCommandAsync( UpdateUserCommand request,
        CancellationToken cancellationToken ) =>
        await _facade.UpdateAsync( request, _userId, _stadiumGroupId );
}
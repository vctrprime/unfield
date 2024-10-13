using AutoMapper;
using Unfield.Domain;
using Unfield.Domain.Services.Identity;
using Unfield.DTO.Accounts.Users;
using Unfield.Commands.Accounts.Users;
using Unfield.Handlers.Facades.Accounts.Users;

namespace Unfield.Handlers.Handlers.Accounts.Users;

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
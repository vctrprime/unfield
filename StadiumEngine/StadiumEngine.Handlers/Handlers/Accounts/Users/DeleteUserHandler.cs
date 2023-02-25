using AutoMapper;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Services;
using StadiumEngine.Domain.Services.Facades.Accounts;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Accounts.Users;
using StadiumEngine.Handlers.Commands.Accounts.Users;

namespace StadiumEngine.Handlers.Handlers.Accounts.Users;

internal sealed class DeleteUserHandler : BaseRequestHandler<DeleteUserCommand, DeleteUserDto>
{
    private readonly IUserFacade _userFacade;

    public DeleteUserHandler(
        IUserFacade userFacade,
        IMapper mapper, 
        IClaimsIdentityService claimsIdentityService, 
        IUnitOfWork unitOfWork) : base(mapper, claimsIdentityService, unitOfWork)
    {
        _userFacade = userFacade;
    }
    
    public override async ValueTask<DeleteUserDto> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        await _userFacade.DeleteUser(request.UserId, _legalId, _userId);
        await UnitOfWork.SaveChanges();

        return new DeleteUserDto();
    }
}
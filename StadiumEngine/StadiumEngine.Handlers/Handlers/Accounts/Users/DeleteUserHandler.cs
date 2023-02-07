using AutoMapper;
using StadiumEngine.Common;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Repositories.Accounts;
using StadiumEngine.Domain.Services;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Accounts;
using StadiumEngine.DTO.Accounts.Users;
using StadiumEngine.Handlers.Commands.Accounts;
using StadiumEngine.Handlers.Commands.Accounts.Users;

namespace StadiumEngine.Handlers.Handlers.Accounts.Users;

internal sealed class DeleteUserHandler : BaseRequestHandler<DeleteUserCommand, DeleteUserDto>
{
    private readonly IUserRepository _repository;

    public DeleteUserHandler(IMapper mapper, IClaimsIdentityService claimsIdentityService, IUnitOfWork unitOfWork, IUserRepository repository) : base(mapper, claimsIdentityService, unitOfWork)
    {
        _repository = repository;
    }
    
    public override async ValueTask<DeleteUserDto> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.Get(request.UserId);
        
        if (user == null || _legalId != user.LegalId) throw new DomainException(ErrorsKeys.UserNotFound);
        
        if (user.IsSuperuser) throw new DomainException(ErrorsKeys.CantDeleteSuperuser);

        user.UserModifiedId = _userId;
        user.PhoneNumber = $"{user.PhoneNumber}.deleted-by-{_userId}.{DateTime.Now.Ticks}";
        
        _repository.Remove(user);
        await UnitOfWork.SaveChanges();

        return new DeleteUserDto();
    }
}
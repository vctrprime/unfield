using AutoMapper;
using StadiumEngine.Domain.Repositories.Accounts;
using StadiumEngine.Domain.Services;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Accounts;
using StadiumEngine.Handlers.Commands.Accounts;

namespace StadiumEngine.Handlers.Handlers.Accounts;
internal sealed class AuthorizeUserHandler : BaseRequestHandler<AuthorizeUserCommand, AuthorizeUserDto?>
{
    private readonly IUserRepository _repository;

    public AuthorizeUserHandler(IMapper mapper, IClaimsIdentityService claimsIdentityService, IUnitOfWork unitOfWork, IUserRepository repository) 
        : base(mapper, claimsIdentityService, unitOfWork)
    {
        _repository = repository;
    }

    public override async ValueTask<AuthorizeUserDto?> Handle(AuthorizeUserCommand request, CancellationToken cancellationToken)
    {
        if (request.Login == null || request.Password == null) return null;
        
        var user = await _repository.Get(request.Login, request.Password);

        if (user == null) return null;
        
        user.LastLoginDate = DateTime.Now.ToUniversalTime();
        user.UserModifiedId = user.Id;
        
        await _repository.Update(user);
        await UnitOfWork.Commit();

        var userDto = Mapper.Map<AuthorizeUserDto>(user);
        
        return userDto;
    }
}
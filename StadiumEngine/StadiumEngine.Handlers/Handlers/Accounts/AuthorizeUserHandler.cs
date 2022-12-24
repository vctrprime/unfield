using AutoMapper;
using Mediator;
using StadiumEngine.DTO.Accounts;
using StadiumEngine.Handlers.Commands.Accounts;
using StadiumEngine.Repositories.Abstract.Accounts;
using StadiumEngine.Services.Auth.Abstract;

namespace StadiumEngine.Handlers.Handlers.Accounts;
internal sealed class AuthorizeUserHandler : BaseRequestHandler<AuthorizeUserCommand, AuthorizeUserDto?>
{
    private readonly IUserRepository _repository;

    public AuthorizeUserHandler(IMapper mapper, IClaimsIdentityService claimsIdentityService, IUserRepository repository) 
        : base(mapper, claimsIdentityService)
    {
        _repository = repository;
    }

    public override async ValueTask<AuthorizeUserDto?> Handle(AuthorizeUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.Get(request.Login, request.Password);

        if (user == null) return null;
        
        user.LastLoginDate = DateTime.Now.ToUniversalTime();
        user.UserModifiedId = user.Id;
        
        user = await _repository.Update(user);
        
        var userDto = Mapper.Map<AuthorizeUserDto>(user);
        
        return userDto;
    }
}
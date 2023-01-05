using AutoMapper;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Repositories.Accounts;
using StadiumEngine.Domain.Services;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.Domain.Services.Infrastructure;
using StadiumEngine.DTO.Accounts;
using StadiumEngine.Handlers.Commands.Accounts;

namespace StadiumEngine.Handlers.Handlers.Accounts;
internal sealed class AuthorizeUserHandler : BaseRequestHandler<AuthorizeUserCommand, AuthorizeUserDto?>
{
    private readonly IUserRepository _repository;
    private readonly IHasher _hasher;

    public AuthorizeUserHandler(IMapper mapper, IClaimsIdentityService claimsIdentityService, 
        IUnitOfWork unitOfWork, IUserRepository repository,
        IHasher hasher) 
        : base(mapper, claimsIdentityService, unitOfWork)
    {
        _repository = repository;
        _hasher = hasher;
    }

    public override async ValueTask<AuthorizeUserDto?> Handle(AuthorizeUserCommand request, CancellationToken cancellationToken)
    {
        if (request.Login == null || request.Password == null) return null;
        
        var user = await _repository.Get(request.Login);

        if (user == null) throw new DomainException("Invalid phone number");

        if (!_hasher.Check(user.Password, request.Password)) throw new DomainException("Invalid password");
        
        user.LastLoginDate = DateTime.Now.ToUniversalTime();
        
        _repository.Update(user);
        await UnitOfWork.SaveChanges();

        var userDto = Mapper.Map<AuthorizeUserDto>(user);
        
        return userDto;
    }
}
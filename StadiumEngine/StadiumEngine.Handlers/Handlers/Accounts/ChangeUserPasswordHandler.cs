using AutoMapper;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Repositories.Accounts;
using StadiumEngine.Domain.Services;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.Domain.Services.Infrastructure;
using StadiumEngine.DTO.Accounts;
using StadiumEngine.Handlers.Commands.Accounts;

namespace StadiumEngine.Handlers.Handlers.Accounts;

internal sealed class ChangeUserPasswordHandler : BaseRequestHandler<ChangeUserPasswordCommand, ChangeUserPasswordDto>
{
    private readonly IUserRepository _repository;
    private readonly IHasher _hasher;
    private readonly IPasswordValidator _passwordValidator;

    public ChangeUserPasswordHandler(IMapper mapper, IClaimsIdentityService? claimsIdentityService, IUnitOfWork unitOfWork, 
        IUserRepository repository, IHasher hasher, IPasswordValidator passwordValidator) : base(mapper, claimsIdentityService, unitOfWork)
    {
        _repository = repository;
        _hasher = hasher;
        _passwordValidator = passwordValidator;
    }
    
    public override async ValueTask<ChangeUserPasswordDto> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
    {
        if (request.NewPassword != request.NewPasswordRepeat) throw new DomainException("Пароли не совпадают!");
        
        if (!_passwordValidator.Validate(request.NewPassword)) throw new DomainException("Пароль не соответствует условиям!");
        
        var user = await _repository.Get(_userId);
        if (user == null) throw new DomainException("User not found!");
        
        if (!_hasher.Check(user.Password, request.OldPassword)) throw new DomainException("Invalid password");
        
        user.Password = _hasher.Crypt(request.NewPassword);
        _repository.Update(user);

        await UnitOfWork.SaveChanges();

        return new ChangeUserPasswordDto();
    }
}
using AutoMapper;
using StadiumEngine.Common;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Repositories.Accounts;
using StadiumEngine.Domain.Services;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.Domain.Services.Infrastructure;
using StadiumEngine.DTO.Accounts;
using StadiumEngine.DTO.Accounts.Users;
using StadiumEngine.Handlers.Commands.Accounts;
using StadiumEngine.Handlers.Commands.Accounts.Users;

namespace StadiumEngine.Handlers.Handlers.Accounts.Users;

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
        if (request.NewPassword != request.NewPasswordRepeat) throw new DomainException(ErrorsKeys.PasswordsNotEqual);
        
        if (!_passwordValidator.Validate(request.NewPassword)) throw new DomainException(ErrorsKeys.PasswordDoesntMatchConditions);
        
        var user = await _repository.Get(_userId);
        if (user == null) throw new DomainException(ErrorsKeys.UserNotFound);
        
        if (!_hasher.Check(user.Password, request.OldPassword)) throw new DomainException(ErrorsKeys.InvalidPassword);
        
        user.Password = _hasher.Crypt(request.NewPassword);
        _repository.Update(user);

        await UnitOfWork.SaveChanges();

        return new ChangeUserPasswordDto();
    }
}
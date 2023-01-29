using AutoMapper;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Repositories.Accounts;
using StadiumEngine.Domain.Services;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.Domain.Services.Infrastructure;
using StadiumEngine.DTO.Accounts;
using StadiumEngine.Handlers.Commands.Accounts;

namespace StadiumEngine.Handlers.Handlers.Accounts;

internal sealed class ResetUserPasswordHandler : BaseRequestHandler<ResetUserPasswordCommand, ResetUserPasswordDto>
{
    private readonly IUserRepository _repository;
    private readonly IHasher _hasher;
    private readonly IPasswordGenerator _passwordGenerator;
    private readonly IPhoneNumberChecker _phoneNumberChecker;
    private readonly ISmsSender _smsSender;

    public ResetUserPasswordHandler(IMapper mapper, IUnitOfWork unitOfWork, 
        IUserRepository repository, IHasher hasher, IPasswordGenerator passwordGenerator,
        IPhoneNumberChecker phoneNumberChecker, ISmsSender smsSender) : base(mapper, null, unitOfWork)
    {
        _repository = repository;
        _hasher = hasher;
        _passwordGenerator = passwordGenerator;
        _phoneNumberChecker = phoneNumberChecker;
        _smsSender = smsSender;
    }

    public override async ValueTask<ResetUserPasswordDto> Handle(ResetUserPasswordCommand request, CancellationToken cancellationToken)
    {
        request.PhoneNumber = _phoneNumberChecker.Check(request.PhoneNumber);
        
        var user = await _repository.Get(request.PhoneNumber);
        if (user == null) throw new DomainException("User not found!");
        
        var userPassword = _passwordGenerator.Generate(8);
        user.Password = _hasher.Crypt(userPassword);
        
        _repository.Update(user);

        await UnitOfWork.SaveChanges();
        
        await _smsSender.Send(request.PhoneNumber,
            $"Ваш пароль для входа: {userPassword}");

        return new ResetUserPasswordDto();
    }
}
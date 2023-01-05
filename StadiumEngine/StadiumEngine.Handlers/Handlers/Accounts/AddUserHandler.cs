using AutoMapper;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Repositories.Accounts;
using StadiumEngine.Domain.Services;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Accounts;
using StadiumEngine.Handlers.Builders.Accounts;
using StadiumEngine.Handlers.Commands.Accounts;
using StadiumEngine.Handlers.Containers.Accounts;

namespace StadiumEngine.Handlers.Handlers.Accounts;

internal sealed class AddUserHandler : BaseRequestHandler<AddUserCommand, AddUserDto>
{
    private readonly AddUserHandlerServicesContainer _servicesContainer;
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;

    public AddUserHandler(IMapper mapper, IClaimsIdentityService claimsIdentityService, IUnitOfWork unitOfWork, AddUserHandlerServicesContainer servicesContainer, IUserRepository userRepository, IRoleRepository roleRepository) : base(mapper, claimsIdentityService, unitOfWork)
    {
        _servicesContainer = servicesContainer;
        _userRepository = userRepository;
        _roleRepository = roleRepository;
    }
    
    public override async ValueTask<AddUserDto> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        request.PhoneNumber = _servicesContainer.PhoneNumberChecker.Check(request.PhoneNumber);
        
        var legalId = ClaimsIdentityService.GetLegalId();
        var userId = ClaimsIdentityService.GetUserId();
        
        var role = await _roleRepository.Get(request.RoleId);
        
        if (role == null || legalId != role.LegalId) throw new DomainException("Указанная роль не найдена!");

        var builder = new NewUserBuilder(Mapper, _servicesContainer.PasswordGenerator, _servicesContainer.Hasher);
        var (user, password) = builder.Build(request, legalId, userId);
        
        _userRepository.Add(user);
        await UnitOfWork.SaveChanges();
        
        await _servicesContainer.SmsSender.Send(request.PhoneNumber,
            $"Ваш пароль для входа: {password}");

        return new AddUserDto();
    }
}
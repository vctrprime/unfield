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
        
        var role = await _roleRepository.Get(request.RoleId);
        CheckRoleAccess(role);

        if (!role.RoleStadiums.Any())
            throw new DomainException("Нельзя указать для пользователя роль, не имеющую связи с объектами!");

        var builder = new NewUserBuilder(Mapper, _servicesContainer.PasswordGenerator, _servicesContainer.Hasher);
        var (user, password) = builder.Build(request, _legalId, _userId);
        
        _userRepository.Add(user);
        await UnitOfWork.SaveChanges();
        
        await _servicesContainer.SmsSender.Send(request.PhoneNumber,
            $"Ваш пароль для входа: {password}");

        return new AddUserDto();
    }
}
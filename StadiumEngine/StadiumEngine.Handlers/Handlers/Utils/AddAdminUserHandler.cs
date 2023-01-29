using AutoMapper;
using StadiumEngine.Common;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Repositories.Accounts;
using StadiumEngine.Domain.Services;
using StadiumEngine.DTO.Utils;
using StadiumEngine.Handlers.Builders.Accounts;
using StadiumEngine.Handlers.Commands.Utils;
using StadiumEngine.Handlers.Containers.Accounts;

namespace StadiumEngine.Handlers.Handlers.Utils;

internal sealed class AddAdminUserHandler : BaseRequestHandler<AddAdminUserCommand, AddAdminUserDto>
{
    private readonly AddUserHandlerServicesContainer _servicesContainer;
    private readonly IUserRepository _userRepository;
    private readonly ILegalRepository _legalRepository;

    public AddAdminUserHandler(IMapper mapper, 
        IUnitOfWork unitOfWork, AddUserHandlerServicesContainer servicesContainer, 
        IUserRepository userRepository, ILegalRepository legalRepository) : base(mapper, null, unitOfWork)
    {
        _servicesContainer = servicesContainer;
        _userRepository = userRepository;
        _legalRepository = legalRepository;
    }

    public override async ValueTask<AddAdminUserDto> Handle(AddAdminUserCommand request, CancellationToken cancellationToken)
    {
        request.PhoneNumber = _servicesContainer.PhoneNumberChecker.Check(request.PhoneNumber);

        var userSameNumber = await _userRepository.Get(request.PhoneNumber);
        if (userSameNumber != null) throw new DomainException("Пользователь с таким номером телефона уже существует");

        var legalId = await _legalRepository.GetByFilter(string.Empty);
        
        var builder = new NewUserBuilder(Mapper, _servicesContainer.PasswordGenerator, _servicesContainer.Hasher);
        var (user, password) = builder.Build(request, legalId.First().Id);
        user.IsAdmin = true;
        user.IsSuperuser = true;
        user.RoleId = null;
        
        _userRepository.Add(user);
        await UnitOfWork.SaveChanges();
        
        await _servicesContainer.SmsSender.SendPassword(request.PhoneNumber, password, user.Language);

        return new AddAdminUserDto();
    }
}
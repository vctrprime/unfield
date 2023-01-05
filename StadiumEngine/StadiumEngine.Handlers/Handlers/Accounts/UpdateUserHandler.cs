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

internal sealed class UpdateUserHandler : BaseRequestHandler<UpdateUserCommand, UpdateUserDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;

    public UpdateUserHandler(IMapper mapper, IClaimsIdentityService claimsIdentityService, IUnitOfWork unitOfWork, IUserRepository userRepository, IRoleRepository roleRepository) : base(mapper, claimsIdentityService, unitOfWork)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
    }
    
    public override async ValueTask<UpdateUserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        if (request.Id == _userId) throw new DomainException("Нельзя изменять текущего пользователя!");
        
        var role = await _roleRepository.Get(request.RoleId);
        CheckRoleAccess(role);
        
        if (!role.RoleStadiums.Any())
            throw new DomainException("Нельзя указать для пользователя роль, не имеющую связи с объектами!");

        var user = await _userRepository.Get(request.Id);
        if (user == null || _legalId != user.LegalId) throw new DomainException("Указанный пользователь не найден!");

        user.Name = request.Name;
        user.Description = request.Description;
        user.RoleId = request.RoleId;
        user.LastName = request.LastName;
        
        _userRepository.Update(user);
        await UnitOfWork.SaveChanges();

        return new UpdateUserDto();
    }
}
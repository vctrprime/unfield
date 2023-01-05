using AutoMapper;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Repositories.Accounts;
using StadiumEngine.Domain.Services;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Accounts;
using StadiumEngine.Handlers.Commands.Accounts;

namespace StadiumEngine.Handlers.Handlers.Accounts;

internal sealed class UpdateRoleHandler : BaseRequestHandler<UpdateRoleCommand, UpdateRoleDto>
{
    private readonly IRoleRepository _roleRepository;
    private readonly IUserRepository _userRepository;

    public UpdateRoleHandler(IMapper mapper, IClaimsIdentityService claimsIdentityService, 
        IUnitOfWork unitOfWork, 
        IRoleRepository roleRepository, IUserRepository userRepository) : base(mapper, claimsIdentityService, unitOfWork)
    {
        _roleRepository = roleRepository;
        _userRepository = userRepository;
    }
    
    public override async ValueTask<UpdateRoleDto> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.Get(_userId);
        
        if (user?.RoleId == request.Id)
            throw new DomainException("Нельзя править текущую роль пользователя!");
        
        var role = await _roleRepository.Get(request.Id);
        CheckRoleAccess(role);

        role.Name = request.Name;
        role.Description = request.Description;
        role.UserModifiedId = _userId;
        
        _roleRepository.Update(role);
        await UnitOfWork.SaveChanges();

        return new UpdateRoleDto();
    }
}
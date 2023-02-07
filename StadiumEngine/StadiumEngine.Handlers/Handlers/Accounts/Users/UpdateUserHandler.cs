using AutoMapper;
using StadiumEngine.Common;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Repositories.Accounts;
using StadiumEngine.Domain.Services;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Accounts;
using StadiumEngine.DTO.Accounts.Users;
using StadiumEngine.Handlers.Commands.Accounts;
using StadiumEngine.Handlers.Commands.Accounts.Users;

namespace StadiumEngine.Handlers.Handlers.Accounts.Users;

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
        if (request.Id == _userId) throw new DomainException(ErrorsKeys.ModifyCurrentUser);
        
        var role = await _roleRepository.Get(request.RoleId);
        CheckRoleAccess(role);
        
        if (!role.RoleStadiums.Any())
            throw new DomainException(ErrorsKeys.UserRolesDoesntHaveStadiums);

        var user = await _userRepository.Get(request.Id);
        if (user == null || _legalId != user.LegalId) throw new DomainException(ErrorsKeys.UserNotFound);

        user.Name = request.Name;
        user.Description = request.Description;
        user.RoleId = request.RoleId;
        user.LastName = request.LastName;
        
        _userRepository.Update(user);
        await UnitOfWork.SaveChanges();

        return new UpdateUserDto();
    }
}
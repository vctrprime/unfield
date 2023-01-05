using AutoMapper;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Repositories.Accounts;
using StadiumEngine.Domain.Services;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Accounts;
using StadiumEngine.Handlers.Commands.Accounts;

namespace StadiumEngine.Handlers.Handlers.Accounts;

internal sealed class ToggleRoleStadiumHandler : BaseRequestHandler<ToggleRoleStadiumCommand, ToggleRoleStadiumDto>
{
    private readonly IRoleStadiumRepository _roleStadiumRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IUserRepository _userRepository;
    private readonly IStadiumRepository _stadiumRepository;

    public ToggleRoleStadiumHandler(IMapper mapper, IClaimsIdentityService claimsIdentityService, 
        IUnitOfWork unitOfWork, IRoleStadiumRepository roleStadiumRepository, 
        IRoleRepository roleRepository, IUserRepository userRepository,
        IStadiumRepository stadiumRepository) : base(mapper, claimsIdentityService, unitOfWork)
    {
        _roleStadiumRepository = roleStadiumRepository;
        _roleRepository = roleRepository;
        _userRepository = userRepository;
        _stadiumRepository = stadiumRepository;
    }
    
    
    public override async ValueTask<ToggleRoleStadiumDto> Handle(ToggleRoleStadiumCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.Get(_userId);
        
        if (user?.RoleId == request.RoleId)
            throw new DomainException("Нельзя править объекты для роли текущего пользователя!");
        
        var role = await _roleRepository.Get(request.RoleId);
        CheckRoleAccess(role);

        var stadiums = await _stadiumRepository.GetForLegal(_legalId);
        var stadium = stadiums.FirstOrDefault(s => s.Id == request.StadiumId);
        
        if (stadium == null) throw new DomainException("Указанный объект не найден!");
        
        var roleStadium = await _roleStadiumRepository.Get(request.RoleId, request.StadiumId);
        if (roleStadium == null)
        {
            roleStadium = Mapper.Map<RoleStadium>(request);
            roleStadium.UserCreatedId = _userId;
            _roleStadiumRepository.Add(roleStadium);
        }
        else
        {
            if (role.Users.Any() 
                && !(role.RoleStadiums.Any(rs => rs.StadiumId != request.StadiumId)))
            {
                throw new DomainException($"Нельзя отвязать от роли последний объект, если у роли имеются связанные пользователи ({role.Users.Count})!");
            }
            _roleStadiumRepository.Remove(roleStadium);
        }

        await UnitOfWork.SaveChanges();

        return new ToggleRoleStadiumDto();
    }
}
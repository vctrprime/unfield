using AutoMapper;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Repositories.Accounts;
using StadiumEngine.Domain.Services;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Accounts;
using StadiumEngine.Handlers.Commands.Accounts;

namespace StadiumEngine.Handlers.Handlers.Accounts;

internal sealed class DeleteRoleHandler : BaseRequestHandler<DeleteRoleCommand, DeleteRoleDto>
{
    private readonly IRoleRepository _repository;

    public DeleteRoleHandler(IMapper mapper, IClaimsIdentityService claimsIdentityService, 
        IUnitOfWork unitOfWork, IRoleRepository repository) : base(mapper, claimsIdentityService, unitOfWork)
    {
        _repository = repository;
    }


    public override async ValueTask<DeleteRoleDto> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await _repository.Get(request.RoleId);
        CheckRoleAccess(role);

        if (role.RoleStadiums.Any() || role.Users.Any())
            throw new DomainException(
                $"Указанная роль имеет связанные сущности (пользователи: {role.Users.Count}, объекты: {role.RoleStadiums.Count})!");
        
        _repository.Remove(role);
        await UnitOfWork.SaveChanges();

        return new DeleteRoleDto();
    }
}
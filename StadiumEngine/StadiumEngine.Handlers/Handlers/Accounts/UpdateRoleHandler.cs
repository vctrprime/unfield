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
    private readonly IRoleRepository _repository;

    public UpdateRoleHandler(IMapper mapper, IClaimsIdentityService claimsIdentityService, IUnitOfWork unitOfWork, IRoleRepository repository) : base(mapper, claimsIdentityService, unitOfWork)
    {
        _repository = repository;
    }
    
    public override async ValueTask<UpdateRoleDto> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await _repository.Get(request.Id);
        CheckRoleAccess(role);

        role.Name = request.Name;
        role.Description = request.Description;
        role.UserModifiedId = _userId;
        
        _repository.Update(role);
        await UnitOfWork.SaveChanges();

        return new UpdateRoleDto();
    }
}
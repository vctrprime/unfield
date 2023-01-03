using AutoMapper;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Services;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Utils;
using StadiumEngine.Handlers.Commands.Utils;
using StadiumEngine.Handlers.Containers;
using StadiumEngine.Handlers.Containers.Utils;

namespace StadiumEngine.Handlers.Handlers.Utils;

internal sealed class AddLegalHandler : BaseRequestHandler<AddLegalCommand, AddLegalDto>
{
    private readonly AddLegalHandlerRepositoriesContainer _repositoriesContainer;
    private readonly AddLegalHandlerServicesContainer _servicesContainer;
    
    public AddLegalHandler(IMapper mapper, IClaimsIdentityService claimsIdentityService, IUnitOfWork unitOfWork, 
        AddLegalHandlerRepositoriesContainer repositoriesContainer,
        AddLegalHandlerServicesContainer servicesContainer) : base(mapper, claimsIdentityService, unitOfWork)
    {
        _repositoriesContainer = repositoriesContainer;
        _servicesContainer = servicesContainer;
    }

    public override async ValueTask<AddLegalDto> Handle(AddLegalCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await UnitOfWork.BeginTransaction();
            
            //добавляем легал
            var legal = await AddLegal(request);
            //добавляем роль Администратор
            var role = await AddBaseRole(legal.Id);
            //добавляем связь пермишеннов и ролей
            await AddRolePermissions(role.Id);
            //добавляем стадионы
            var stadiums = await AddStadiums(request, legal.Id, legal.CityId);
            //добавляем связь стадинов и роли
            await AddRoleStadiums(stadiums, role.Id);
            //добавляем суперюзера
            await AddSuperuser(request, legal.Id);
            
            await UnitOfWork.CommitTransaction();

            var legalDto = Mapper.Map<AddLegalDto>(legal);
            return legalDto;
        }
        catch
        {
            await UnitOfWork.RollbackTransaction();
            throw;
        }
    }

    private async Task<Legal> AddLegal(AddLegalCommand request)
    {
        var legal = Mapper.Map<Legal>(request);
        _repositoriesContainer.LegalRepository.Add(legal);
        await UnitOfWork.SaveChanges();

        return legal;
    }

    private async Task<Role> AddBaseRole(int legalId)
    {
        var role = new Role
        {
            Name = "Администратор",
            Description = "Базовая роль для администратора (добавлена автоматически)",
            LegalId = legalId,
        };
        _repositoriesContainer.RoleRepository.Add(role);
        await UnitOfWork.SaveChanges();

        return role;
    }

    private async Task AddRolePermissions(int roleId)
    {
        var permissions = await _repositoriesContainer.PermissionRepository.GetAll();
        var permissionsKeys = new List<string>
        {
            "schedule", "actives"
        };
            
        var rolePermissions = permissions.Where(p => permissionsKeys.Contains(p.PermissionGroup.Key)).Select(p => new RolePermission()
        {
            RoleId = roleId,
            PermissionId = p.Id
        });
        _repositoriesContainer.RolePermissionRepository.Add(rolePermissions);
        await UnitOfWork.SaveChanges();
    }

    private async Task<List<Stadium>> AddStadiums(AddLegalCommand request, int legalId, int cityId)
    {
        var stadiums = Mapper.Map<List<Stadium>>(request.Stadiums);
        stadiums.ForEach(s =>
        {
            s.LegalId = legalId;
            s.CityId = cityId;
        });
        _repositoriesContainer.StadiumRepository.Add(stadiums);
        await UnitOfWork.SaveChanges();

        return stadiums;
    }

    private async Task AddRoleStadiums(List<Stadium> stadiums, int roleId)
    {
        var roleStadiums = stadiums.Select(s => new RoleStadium
        {
            StadiumId = s.Id,
            RoleId = roleId
        });
        _repositoriesContainer.RoleStadiumRepository.Add(roleStadiums);
        await UnitOfWork.SaveChanges();

    }

    private async Task AddSuperuser(AddLegalCommand request, int legalId)
    {
        var user = Mapper.Map<User>(request.Superuser);
        
        user.Password = _servicesContainer.PasswordGenerator.Generate(8);
        user.LegalId = legalId;
            
        _repositoriesContainer.UserRepository.Add(user);
        await UnitOfWork.SaveChanges();
    }
}
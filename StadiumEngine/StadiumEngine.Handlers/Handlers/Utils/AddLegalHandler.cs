using AutoMapper;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Services;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Utils;
using StadiumEngine.Handlers.Builders.Utils;
using StadiumEngine.Handlers.Commands.Utils;
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
        Legal legal;
        string superuserPassword;
        
        try
        {
            await UnitOfWork.BeginTransaction();
            
            (legal, superuserPassword) = await AddLegal(request);
            
            await UnitOfWork.CommitTransaction();
            
        }
        catch
        {
            await UnitOfWork.RollbackTransaction();
            throw;
        }

        await _servicesContainer.SmsSender.Send(request.Superuser.PhoneNumber,
            $"Ваш пароль для входа: {superuserPassword}");
        var legalDto = Mapper.Map<AddLegalDto>(legal);
        return legalDto;
        
    }

    private async Task<(Legal, string)> AddLegal(AddLegalCommand request)
    {
        var builder = new NewLegalBuilder(Mapper, _repositoriesContainer.PermissionRepository, _servicesContainer.PasswordGenerator,
            _servicesContainer.Hasher);

        var (legal, password) = await builder.Build(request);
        
        _repositoriesContainer.LegalRepository.Add(legal);
        await UnitOfWork.SaveChanges();

        return (legal, password);
    }
    
   
}
using AutoMapper;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Repositories.Accounts;
using StadiumEngine.Domain.Services;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Utils;
using StadiumEngine.Handlers.Builders.Utils;
using StadiumEngine.Handlers.Commands.Utils;
using StadiumEngine.Handlers.Containers.Utils;

namespace StadiumEngine.Handlers.Handlers.Utils;

internal sealed class AddLegalHandler : BaseRequestHandler<AddLegalCommand, AddLegalDto>
{
    private readonly IPermissionRepository _permissionRepository;
    private readonly ILegalRepository _legalRepository;
    private readonly AddLegalHandlerServicesContainer _servicesContainer;
    
    public AddLegalHandler(IMapper mapper, IUnitOfWork unitOfWork, 
        IPermissionRepository permissionRepository,
        ILegalRepository legalRepository,
        AddLegalHandlerServicesContainer servicesContainer) : base(mapper, null, unitOfWork)
    {
        _permissionRepository = permissionRepository;
        _legalRepository = legalRepository;
        _servicesContainer = servicesContainer;
    }

    public override async ValueTask<AddLegalDto> Handle(AddLegalCommand request, CancellationToken cancellationToken)
    {
        if (!request.Stadiums.Any()) throw new DomainException("Передан пустой список объектов для добавления!");
        
        request.Superuser.PhoneNumber = _servicesContainer.PhoneNumberChecker.Check(request.Superuser.PhoneNumber);
        
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
        var builder = new NewLegalBuilder(Mapper, _permissionRepository, _servicesContainer.PasswordGenerator,
            _servicesContainer.Hasher);

        var (legal, password) = await builder.Build(request);
        
        _legalRepository.Add(legal);
        await UnitOfWork.SaveChanges();

        return (legal, password);
    }
    
   
}
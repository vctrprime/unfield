using AutoMapper;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Services;
using StadiumEngine.Domain.Services.Facades.Accounts;
using StadiumEngine.Domain.Services.Infrastructure;
using StadiumEngine.DTO.Utils;
using StadiumEngine.Handlers.Commands.Utils;

namespace StadiumEngine.Handlers.Handlers.Utils;

internal sealed class AddLegalHandler : BaseRequestHandler<AddLegalCommand, AddLegalDto>
{
    private readonly ILegalFacade _legalFacade;
    private readonly ISmsSender _smsSender;
    
    public AddLegalHandler(
        ILegalFacade legalFacade,
        ISmsSender smsSender,
        IMapper mapper, 
        IUnitOfWork unitOfWork) : base(mapper, null, unitOfWork)
    {
        _legalFacade = legalFacade;
        _smsSender = smsSender;
    }

    public override async ValueTask<AddLegalDto> Handle(AddLegalCommand request, CancellationToken cancellationToken)
    {
        var legal = Mapper.Map<Legal>(request);
        var superuser = Mapper.Map<User>(request.Superuser);
        
        superuser.Language = request.Language;

        var password = await _legalFacade.AddLegal(legal, superuser);
        
        await UnitOfWork.SaveChanges();
        
        await _smsSender.SendPassword(superuser.PhoneNumber,
            password, superuser.Language);
        
        var legalDto = Mapper.Map<AddLegalDto>(legal);
        
        return legalDto;
        
    }
    
}
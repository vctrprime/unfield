using AutoMapper;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Entities.Offers;
using StadiumEngine.Domain.Services;
using StadiumEngine.Domain.Services.Facades.Offers;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Offers.Fields;
using StadiumEngine.Handlers.Commands.Offers.Fields;

namespace StadiumEngine.Handlers.Handlers.Offers.Fields;

internal sealed class AddFieldHandler : BaseRequestHandler<AddFieldCommand, AddFieldDto>
{
    private readonly IFieldFacade _fieldFacade;

    public AddFieldHandler(
        IFieldFacade fieldFacade,
        IMapper mapper, 
        IClaimsIdentityService claimsIdentityService, 
        IUnitOfWork unitOfWork) : base(mapper, claimsIdentityService, unitOfWork)
    {
        _fieldFacade = fieldFacade;
    }
    
    public override async ValueTask<AddFieldDto> Handle(AddFieldCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await UnitOfWork.BeginTransaction();
            
            var field = Mapper.Map<Field>(request);
            field.StadiumId = _currentStadiumId;
            field.UserCreatedId = _userId;

            await _fieldFacade.AddField(field, request.Images, _legalId);
            
            await UnitOfWork.CommitTransaction();

            return new AddFieldDto();

        }
        catch
        {
            await UnitOfWork.RollbackTransaction();
            throw;
        }
    }
}
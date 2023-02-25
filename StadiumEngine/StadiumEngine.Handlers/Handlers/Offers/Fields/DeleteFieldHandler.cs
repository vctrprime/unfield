using AutoMapper;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Services;
using StadiumEngine.Domain.Services.Facades.Offers;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Offers.Fields;
using StadiumEngine.Handlers.Commands.Offers.Fields;

namespace StadiumEngine.Handlers.Handlers.Offers.Fields;

internal sealed class DeleteFieldHandler : BaseRequestHandler<DeleteFieldCommand, DeleteFieldDto>
{
    private readonly IFieldFacade _fieldFacade;
    public DeleteFieldHandler(
        IFieldFacade fieldFacade,
        IMapper mapper, 
        IClaimsIdentityService claimsIdentityService, 
        IUnitOfWork unitOfWork) : base(mapper, claimsIdentityService, unitOfWork)
    {
        _fieldFacade = fieldFacade;
    }

    public override async ValueTask<DeleteFieldDto> Handle(DeleteFieldCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await UnitOfWork.BeginTransaction();
            
            await _fieldFacade.DeleteField(request.FieldId, _currentStadiumId);
            
            await UnitOfWork.CommitTransaction();
        }
        catch
        {
            await UnitOfWork.RollbackTransaction();
            throw;
        }

        return new DeleteFieldDto();
    }
}
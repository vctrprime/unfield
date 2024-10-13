using Unfield.Domain;
using Unfield.Domain.Entities.Offers;
using Unfield.Domain.Repositories.Offers;
using Unfield.Services.Handlers.Offers;

namespace Unfield.Services.Facades.Offers;

internal class FieldServiceFacade : IFieldServiceFacade
{
    private readonly IFieldPriceGroupHandler _fieldPriceGroupHandler;
    private readonly IFieldRepository _fieldRepository;
    private readonly IUnitOfWork _unitOfWork;

    public FieldServiceFacade( IFieldPriceGroupHandler fieldPriceGroupHandler, IFieldRepository fieldRepository,
        IUnitOfWork unitOfWork )
    {
        _fieldPriceGroupHandler = fieldPriceGroupHandler;
        _fieldRepository = fieldRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Field?> GetFieldAsync( int fieldId, int stadiumId ) => await _fieldRepository.GetAsync( fieldId, stadiumId );

    public async Task AddFieldAsync( Field field )
    {
        _fieldRepository.Add( field );
        await _unitOfWork.SaveChangesAsync();
        await _fieldPriceGroupHandler.HandleAsync( field, field.UserCreatedId );
    }

    public async Task UpdateFieldAsync( Field field )
    {
        _fieldRepository.Update( field );
        if ( _unitOfWork.PropertyWasChanged( field, "PriceGroupId" ))
        {
            await _fieldPriceGroupHandler.HandleAsync( field, field.UserModifiedId );
        }
    }

    public void RemoveField( Field field ) => _fieldRepository.Remove( field );
}
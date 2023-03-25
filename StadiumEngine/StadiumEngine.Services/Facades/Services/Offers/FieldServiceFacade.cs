using StadiumEngine.Domain;
using StadiumEngine.Domain.Entities.Offers;
using StadiumEngine.Domain.Repositories.Offers;
using StadiumEngine.Services.Handlers.Offers;

namespace StadiumEngine.Services.Facades.Services.Offers;

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

    public async Task<Field?> GetField( int fieldId, int stadiumId ) => await _fieldRepository.Get( fieldId, stadiumId );

    public async Task AddField( Field field )
    {
        _fieldRepository.Add( field );
        await _unitOfWork.SaveChanges();
        await _fieldPriceGroupHandler.Handle( field, field.UserCreatedId );
    }

    public async Task UpdateField( Field field )
    {
        _fieldRepository.Update( field );
        if ( _unitOfWork.PropertyWasChanged( field, "PriceGroupId" ))
        {
            await _fieldPriceGroupHandler.Handle( field, field.UserModifiedId );
        }
    }

    public void RemoveField( Field field ) => _fieldRepository.Remove( field );
}
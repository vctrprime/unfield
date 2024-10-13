using Unfield.Domain.Entities.Offers;

namespace Unfield.Services.Facades.Offers;

internal interface IFieldServiceFacade
{
    Task<Field?> GetFieldAsync( int fieldId, int stadiumId );
    Task AddFieldAsync( Field field );
    Task UpdateFieldAsync( Field field );
    void RemoveField( Field field );
}
using StadiumEngine.Domain.Entities.Offers;

namespace StadiumEngine.Services.Facades.Offers;

internal interface IFieldServiceFacade
{
    Task<Field?> GetFieldAsync( int fieldId, int stadiumId );
    Task AddFieldAsync( Field field );
    Task UpdateFieldAsync( Field field );
    void RemoveField( Field field );
}
using StadiumEngine.Domain.Entities.Offers;

namespace StadiumEngine.Services.Facades.Services.Offers;

internal interface IFieldServiceFacade
{
    Task<Field?> GetField( int fieldId, int stadiumId );
    Task AddField( Field field );
    Task UpdateField( Field field );
    void RemoveField( Field field );
}
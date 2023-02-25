#nullable enable
using StadiumEngine.Domain.Entities.Offers;

namespace StadiumEngine.Domain.Repositories.Offers;

public interface IFieldRepository
{
    Task<List<Field>> GetAll(int stadiumId);
    Task<Field?> Get(int fieldId, int stadiumId);
    void Add(Field field);
    void Update(Field field);
    void Remove(Field field);
}
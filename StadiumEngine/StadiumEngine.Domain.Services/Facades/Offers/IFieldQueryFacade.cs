#nullable enable
using StadiumEngine.Common.Enums.Offers;
using StadiumEngine.Common.Models;
using StadiumEngine.Domain.Entities.Offers;

namespace StadiumEngine.Domain.Services.Facades.Offers;

public interface IFieldQueryFacade
{
    Task<List<Field>> GetByStadiumId(int stadiumId);
    Task<Field?> GetByFieldId(int fieldId, int stadiumId);
}
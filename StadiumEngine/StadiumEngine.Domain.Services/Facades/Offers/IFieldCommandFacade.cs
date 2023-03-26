#nullable enable
using StadiumEngine.Common.Enums.Offers;
using StadiumEngine.Common.Models;
using StadiumEngine.Domain.Entities.Offers;

namespace StadiumEngine.Domain.Services.Facades.Offers;

public interface IFieldCommandFacade
{
    Task AddFieldAsync( Field field, List<ImageFile> images, int legalId );
    Task UpdateFieldAsync( Field field, List<ImageFile> images, List<SportKind> sportKinds );
    Task DeleteFieldAsync( int fieldId, int stadiumId );
}
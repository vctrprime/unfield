#nullable enable
using StadiumEngine.Common.Enums.Offers;
using StadiumEngine.Common.Models;
using StadiumEngine.Domain.Entities.Offers;

namespace StadiumEngine.Domain.Services.Core.Offers;

public interface IFieldCommandService
{
    Task AddFieldAsync( Field field, List<ImageFile> images, int stadiumGroupId );
    Task UpdateFieldAsync( Field field, List<ImageFile> images, List<SportKind> sportKinds );
    Task DeleteFieldAsync( int fieldId, int stadiumId );
}
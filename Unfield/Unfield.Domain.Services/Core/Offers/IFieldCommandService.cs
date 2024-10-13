#nullable enable
using Unfield.Common.Enums.Offers;
using Unfield.Common.Models;
using Unfield.Domain.Entities.Offers;

namespace Unfield.Domain.Services.Core.Offers;

public interface IFieldCommandService
{
    Task AddFieldAsync( Field field, List<ImageFile> images, int stadiumGroupId );
    Task UpdateFieldAsync( Field field, List<ImageFile> images, List<SportKind> sportKinds );
    Task DeleteFieldAsync( int fieldId, int stadiumId );
}
#nullable enable
using StadiumEngine.Common.Enums.Offers;
using StadiumEngine.Common.Models;
using StadiumEngine.Domain.Entities.Offers;

namespace StadiumEngine.Domain.Services.Facades.Offers;

public interface IFieldCommandFacade
{
    Task AddField(Field field, List<ImageFile> images, int legalId);
    Task UpdateField(Field field, List<ImageFile> images, List<SportKind> sportKinds);
    Task DeleteField(int fieldId, int stadiumId);
}
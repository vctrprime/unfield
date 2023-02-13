using System.Collections.Generic;
using StadiumEngine.Domain.Entities.Offers;

namespace StadiumEngine.Domain.Repositories.Offers;

public interface IFieldSportKindRepository
{
    void Add(IEnumerable<FieldSportKind> sportKinds);
    void Remove(IEnumerable<FieldSportKind> sportKinds);
}
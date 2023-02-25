using StadiumEngine.Domain.Entities.Offers;

namespace StadiumEngine.Domain.Repositories.Offers;

public interface IOffersSportKindRepository
{
    void Add(IEnumerable<OffersSportKind> sportKinds);
    void Remove(IEnumerable<OffersSportKind> sportKinds);
}
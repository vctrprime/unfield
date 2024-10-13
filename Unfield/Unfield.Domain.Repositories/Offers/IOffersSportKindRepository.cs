using Unfield.Domain.Entities.Offers;

namespace Unfield.Domain.Repositories.Offers;

public interface IOffersSportKindRepository
{
    void Add( IEnumerable<OffersSportKind> sportKinds );
    void Remove( IEnumerable<OffersSportKind> sportKinds );
}
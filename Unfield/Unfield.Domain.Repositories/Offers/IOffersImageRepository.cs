using Unfield.Domain.Entities.Offers;

namespace Unfield.Domain.Repositories.Offers;

public interface IOffersImageRepository
{
    void Add( OffersImage image );
    void Update( OffersImage image );
    void Remove( IEnumerable<OffersImage> images );
}
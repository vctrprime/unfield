using Unfield.Domain.Entities.Settings;

namespace Unfield.Domain.Repositories.Settings;

public interface IBreakFieldRepository
{
    void Remove( IEnumerable<BreakField> breakFields );
    void Add( IEnumerable<BreakField> breakFields );
}
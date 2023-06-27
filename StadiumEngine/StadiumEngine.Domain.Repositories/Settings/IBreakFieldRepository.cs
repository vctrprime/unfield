using StadiumEngine.Domain.Entities.Settings;

namespace StadiumEngine.Domain.Repositories.Settings;

public interface IBreakFieldRepository
{
    void Remove( IEnumerable<BreakField> breakFields );
    void Add( IEnumerable<BreakField> breakFields );
}
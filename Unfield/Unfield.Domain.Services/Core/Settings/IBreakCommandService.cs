using Unfield.Domain.Entities.Settings;

namespace Unfield.Domain.Services.Core.Settings;

public interface IBreakCommandService
{
    void AddBreak( Break @break );
    void UpdateBreak( Break @break, List<int> selectedFields );
    Task DeleteBreakAsync( int breakId, int stadiumId );
}
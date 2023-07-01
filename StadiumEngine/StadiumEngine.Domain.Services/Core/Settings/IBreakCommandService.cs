using StadiumEngine.Domain.Entities.Settings;

namespace StadiumEngine.Domain.Services.Core.Settings;

public interface IBreakCommandService
{
    void AddBreak( Break @break );
    void UpdateBreak( Break @break, List<int> selectedFields );
    Task DeleteBreakAsync( int breakId, int stadiumId );
}
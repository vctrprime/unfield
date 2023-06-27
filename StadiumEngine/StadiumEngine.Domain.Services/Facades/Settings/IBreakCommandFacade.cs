using StadiumEngine.Domain.Entities.Settings;

namespace StadiumEngine.Domain.Services.Facades.Settings;

public interface IBreakCommandFacade
{
    void AddBreak( Break @break );
    void UpdateBreak( Break @break, List<int> selectedFields );
    Task DeleteBreakAsync( int breakId, int stadiumId );
}
using Microsoft.EntityFrameworkCore;
using StadiumEngine.Domain.Entities.Settings;
using StadiumEngine.Domain.Repositories.Settings;
using StadiumEngine.Repositories.Infrastructure.Contexts;

namespace StadiumEngine.Repositories.Settings;

internal class MainSettingsRepository : BaseRepository<MainSettings>, IMainSettingsRepository
{
    public MainSettingsRepository( MainDbContext context ) : base( context )
    {
    }

    public async Task<MainSettings> GetAsync( int stadiumId )
    {
        MainSettings? settings = await Entities
            .Include( s => s.Stadium )
            .FirstOrDefaultAsync( x => x.StadiumId == stadiumId );

        if ( settings != null )
        {
            return settings;
        }

        settings = new MainSettings
        {
            StadiumId = stadiumId,
            OpenTime = 8,
            CloseTime = 23
        };
        Entities.Add( settings );
        await Commit();
        
        settings = await Entities
            .Include( s => s.Stadium )
            .FirstAsync( x => x.StadiumId == stadiumId );
        
        return settings;
    }

    public async Task<List<MainSettings>> GetAsync( List<int> stadiumsIds ) => await Entities.Where( x => stadiumsIds.Contains(x.StadiumId) ).ToListAsync();

    public new void Update( MainSettings mainSettings ) => base.Update( mainSettings );
}
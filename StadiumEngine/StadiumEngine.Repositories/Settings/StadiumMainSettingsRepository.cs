using Microsoft.EntityFrameworkCore;
using StadiumEngine.Domain.Entities.Settings;
using StadiumEngine.Domain.Repositories.Settings;
using StadiumEngine.Repositories.Infrastructure.Contexts;

namespace StadiumEngine.Repositories.Settings;

internal class StadiumMainSettingsRepository : BaseRepository<StadiumMainSettings>, IStadiumMainSettingsRepository
{
    public StadiumMainSettingsRepository( MainDbContext context ) : base( context )
    {
    }

    public async Task<StadiumMainSettings> Get( int stadiumId )
    {
        StadiumMainSettings? settings = await Entities.FirstOrDefaultAsync( x => x.StadiumId == stadiumId );

        if ( settings != null )
        {
            return settings;
        }

        settings = new StadiumMainSettings
        {
            StadiumId = stadiumId,
            OpenTime = "08:00",
            CloseTime = "23:00"
        };
        Entities.Add( settings );
        await Commit();
        
        return settings;

    }

    public new void Update( StadiumMainSettings mainSettings ) => base.Update( mainSettings );
}
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using StadiumEngine.Domain.Entities.Bookings;
using StadiumEngine.Domain.Repositories.Bookings;
using StadiumEngine.Repositories.Infrastructure.Contexts;

namespace StadiumEngine.Repositories.Bookings;

internal class BookingRepository : BaseRepository<Booking>, IBookingRepository
{
    public BookingRepository( MainDbContext context ) : base( context )
    {
    }

    public async Task<List<Booking>> GetAsync( DateTime day, List<int> stadiumsIds ) =>
        await Get(
            x => x.Day.Date == day.Date
                 && !x.IsWeekly
                 && stadiumsIds.Contains( x.Field.StadiumId ) );

    public async Task<List<Booking>> GetWeeklyAsync( DateTime day, List<int> stadiumsIds ) =>
        await Get(
            x => x.IsWeekly
                 && ( !x.IsWeeklyStoppedDate.HasValue || x.IsWeeklyStoppedDate > day )
                 && ( !x.Tariff.DateEnd.HasValue || x.Tariff.DateEnd > day )
                 && stadiumsIds.Contains( x.Field.StadiumId ) );

    public async Task<List<Booking>> GetAsync( DateTime from, DateTime to, int stadiumId ) =>
        await GetExtended(
            x => x.Day.Date >= from.Date
                 && x.Day.Date <= to.Date
                 && !x.IsWeekly
                 && x.Field.StadiumId == stadiumId );

    public async Task<List<Booking>> GetWeeklyAsync( DateTime to, int stadiumId ) =>
        await GetExtended(
            x => x.IsWeekly
                 && ( !x.IsWeeklyStoppedDate.HasValue || x.IsWeeklyStoppedDate > to )
                 && ( !x.Tariff.DateEnd.HasValue || x.Tariff.DateEnd > to )
                 && x.Field.StadiumId == stadiumId );

    public async Task<Booking?> GetByNumberAsync( string bookingNumber ) =>
        await Entities
            .Include( x => x.Field )
            .ThenInclude( x => x.SportKinds )
            .Include( x => x.Customer )
            .Include( x => x.Promo )
            .Include( x => x.Costs )
            .Include( x => x.WeeklyExcludeDays )
            .SingleOrDefaultAsync( x => x.Number == bookingNumber && x.IsLastVersion );

    public async Task<Booking?> FindDraftAsync( DateTime day, decimal startHour, int fieldId ) =>
        await Entities.SingleOrDefaultAsync(
            x => x.Day == day 
                 && x.StartHour == startHour 
                 && x.FieldId == fieldId 
                 && !x.IsCanceled 
                 && x.IsLastVersion
                 && x.IsDraft );

    public new void Add( Booking booking ) => base.Add( booking );
    public new void Update( Booking booking ) => base.Update( booking );

    private async Task<List<Booking>> Get( Expression<Func<Booking, bool>> clause ) =>
        await Entities
            .Where( x => x.IsLastVersion )
            .Include( x => x.Field )
            .ThenInclude( x => x.ChildFields )
            .Include( x => x.Tariff )
            .Include( x => x.Costs )
            .Include( x => x.Inventories )
            .Include( x => x.WeeklyExcludeDays )
            .Where( clause )
            .ToListAsync();

    private async Task<List<Booking>> GetExtended( Expression<Func<Booking, bool>> clause ) =>
        await Entities
            .Where( x => x.IsLastVersion )
            .Include( x => x.Field )
            .ThenInclude( x => x.ChildFields )
            .Include( x => x.Tariff )
            .Include( x => x.Costs )
            .Include( x => x.WeeklyExcludeDays )
            .Include( x => x.Inventories )
            .ThenInclude( x => x.Inventory )
            .Include( x => x.Customer )
            .Include( x => x.Promo )
            .Include( x => x.BookingLockerRoom )
            .ThenInclude( blr => blr!.LockerRoom )
            .Where( clause )
            .ToListAsync();
}
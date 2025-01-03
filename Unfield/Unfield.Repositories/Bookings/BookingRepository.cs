using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Unfield.Domain.Entities.Bookings;
using Unfield.Domain.Repositories.Bookings;
using Unfield.Repositories.Infrastructure.Contexts;

namespace Unfield.Repositories.Bookings;

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
                 && day >= x.Day
                 && ( !x.IsWeeklyStoppedDate.HasValue || x.IsWeeklyStoppedDate > day )
                 && ( !x.Tariff.DateEnd.HasValue || x.Tariff.DateEnd > day )
                 && stadiumsIds.Contains( x.Field.StadiumId ) );

    public async Task<List<Booking>> GetAsync(
        DateTime from,
        DateTime to,
        int stadiumId,
        string? customerPhoneNumber ) =>
        await GetExtended(
            x => x.Day.Date >= from.Date
                 && x.Day.Date <= to.Date
                 && !x.IsWeekly
                 && x.Customer.PhoneNumber == ( customerPhoneNumber ?? x.Customer.PhoneNumber )
                 && x.Field.StadiumId == stadiumId );

    public async Task<List<Booking>> GetWeeklyAsync( int stadiumId, string? customerPhoneNumber ) =>
        await GetExtended(
            x => x.IsWeekly
                 && x.Customer.PhoneNumber == ( customerPhoneNumber ?? x.Customer.PhoneNumber )
                 && x.Field.StadiumId == stadiumId );

    public async Task<Booking?> GetByNumberAsync( string bookingNumber ) =>
        await Entities
            .Include( x => x.Field )
            .ThenInclude( x => x.SportKinds )
            .Include( x => x.Tariff )
            .Include( x => x.Customer )
            .Include( x => x.Promo )
            .Include( x => x.Costs )
            .Include( x => x.Inventories )
            .ThenInclude( x => x.Inventory )
            .Include( x => x.WeeklyExcludeDays )
            .Include( x => x.BookingLockerRoom )
            .ThenInclude( blr => blr!.LockerRoom )
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

    public async Task<int> DeleteDraftsByDateAsync( DateTime date, int limit )
    {
        int rows = await Entities
            .Where( x => x.Day.Date < date && x.IsDraft && !x.IsConfirmed )
            .Take( limit )
            .ExecuteDeleteAsync();

        return rows;
    }

    public Booking DetachedClone( Booking booking )
    {
        Booking copy = ( Entities.Entry( booking ).CurrentValues.Clone().ToObject() as Booking )!;

        copy.Costs = booking.Costs;
        copy.Field = booking.Field;
        copy.BookingLockerRoom = booking.BookingLockerRoom;
        copy.Tariff = booking.Tariff;
        copy.Inventories = booking.Inventories;
        copy.Customer = booking.Customer;
        copy.WeeklyExcludeDays = booking.WeeklyExcludeDays;
        copy.Promo = booking.Promo;

        return copy;
    }

    public async Task<List<Booking>> SearchAllByNumberAsync( string bookingNumber, int stadiumId ) =>
        await GetExtended(
            x => x.Number.ToLower().Contains( bookingNumber.ToLower() )
                 && x.IsConfirmed
                 && !x.IsCanceled
                 && x.Field.StadiumId == stadiumId );

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
            .Include( x => x.Field )
            .ThenInclude( x => x.SportKinds )
            .Include( x => x.Field )
            .ThenInclude( x => x.Images )
            .Include( x => x.Tariff )
            .Include( x => x.Costs )
            .Include( x => x.WeeklyExcludeDays )
            .Include( x => x.Inventories )
            .ThenInclude( x => x.Inventory )
            .ThenInclude( x => x.Images )
            .Include( x => x.Customer )
            .Include( x => x.Promo )
            .Include( x => x.BookingLockerRoom )
            .ThenInclude( blr => blr!.LockerRoom )
            .Where( clause )
            .ToListAsync();
}
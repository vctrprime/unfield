using AutoMapper;
using Unfield.Commands.Schedule;
using Unfield.Domain;
using Unfield.Domain.Entities.Bookings;
using Unfield.Domain.Entities.Offers;
using Unfield.Domain.Services.Core.Offers;
using Unfield.Domain.Services.Core.Schedule;
using Unfield.Domain.Services.Identity;
using Unfield.DTO.Schedule;

namespace Unfield.Handlers.Handlers.Schedule;

internal sealed class
    SaveSchedulerBookingDataHandler : BaseCommandHandler<SaveSchedulerBookingDataCommand, SaveSchedulerBookingDataDto>
{
    private readonly ISchedulerBookingQueryService _queryService;
    private readonly ISchedulerBookingCommandService _commandService;
    private readonly IFieldQueryService _fieldQueryService;

    public SaveSchedulerBookingDataHandler(
        ISchedulerBookingQueryService queryService,
        ISchedulerBookingCommandService commandService,
        IFieldQueryService fieldQueryService,
        IClaimsIdentityService claimsIdentityService,
        IMapper mapper,
        IUnitOfWork unitOfWork ) : base( mapper, claimsIdentityService, unitOfWork )
    {
        _queryService = queryService;
        _commandService = commandService;
        _fieldQueryService = fieldQueryService;
    }

    protected override async ValueTask<SaveSchedulerBookingDataDto> HandleCommandAsync(
        SaveSchedulerBookingDataCommand request,
        CancellationToken cancellationToken )
    {
        Booking booking = await _queryService.GetBookingAsync( request.BookingNumber );
        Field? field = null;

        if ( request.MoveData != null )
        {
            field = await _fieldQueryService.GetByFieldIdAsync( request.MoveData.FieldId, _currentStadiumId );
        }
        
        if ( request.IsNew )
        {
            FillBooking( booking, request );
            await _commandService.FillBookingDataAsync( booking, request.AutoLockerRoom );
        }
        else
        {
            if ( !request.IsWeekly )
            {
                Booking newVersion = new Booking
                {
                    Number = booking.Number,
                    Day = request.MoveData?.Day.Date ?? booking.Day.Date,
                    AccessCode = booking.AccessCode,
                    Source = booking.Source,
                    IsLastVersion = true,
                    UserCreatedId = _userId,
                    FieldId = request.MoveData?.FieldId ?? booking.FieldId,
                    PromoDiscount = booking.PromoDiscount,
                    Field = field ?? booking.Field,
                    StartHour = request.MoveData?.StartHour ?? booking.StartHour
                };
                if ( booking.Promo != null )
                {
                    newVersion.Promo = new BookingPromo
                    {
                        Code = booking.Promo.Code,
                        Type = booking.Promo.Type,
                        Value = booking.Promo.Value,
                    };
                }

                FillBooking( newVersion, request );
                await _commandService.AddVersionAsync( newVersion );

                booking.CloseVersionDate = DateTime.Now;
                booking.IsLastVersion = false;
                booking.UserModifiedId = _userId;
                _commandService.UpdateOldVersion( booking );
            }
            else
            {
                if ( request.EditOneInRow )
                {
                    Booking excludeWeekly = new Booking
                    {
                        Number = $"{booking.Number}-E{booking.WeeklyExcludeDays.Count + 1}",
                        AccessCode = booking.AccessCode,
                        Day = request.MoveData?.Day.Date ?? booking.Day.Date,
                        StartHour = request.MoveData?.StartHour ?? booking.StartHour,
                        Source = booking.Source,
                        IsLastVersion = true,
                        UserCreatedId = _userId,
                        FieldId = request.MoveData?.FieldId ?? booking.FieldId,
                        Field = field ?? booking.Field
                    };
                    FillBooking( excludeWeekly, request );
                    excludeWeekly.IsWeekly = false;
                    await _commandService.AddVersionAsync( excludeWeekly );

                    _commandService.AddExcludeDay( booking.Id, request.Day, _userId );
                }
                else
                {
                    Booking newWeekly = new Booking
                    {
                        Number = GetWeeklyBookingNumber( booking.Number ),
                        AccessCode = booking.AccessCode,
                        StartHour = request.MoveData?.StartHour ?? booking.StartHour,
                        Day = GetNewWeeklyDay( request.ClientDate, booking.Day, booking.WeeklyExcludeDays.Select( x => x.Day ).ToList() ),
                        Source = booking.Source,
                        IsLastVersion = true,
                        UserCreatedId = _userId,
                        FieldId = request.MoveData?.FieldId ?? booking.FieldId,
                        Field = field ?? booking.Field,
                        WeeklyExcludeDays = booking.WeeklyExcludeDays.Select(
                            x => new BookingWeeklyExcludeDay
                            {
                                UserCreatedId = _userId,
                                Day = x.Day,
                                Reason = x.Reason
                            } ).ToList()
                    };
                    FillBooking( newWeekly, request );
                    await _commandService.AddVersionAsync( newWeekly );

                    booking.IsWeeklyStoppedDate = request.ClientDate;
                    booking.UserModifiedId = _userId;
                    _commandService.UpdateOldVersion( booking );
                }
            }
        }

        return new SaveSchedulerBookingDataDto();
    }

    private DateTime GetNewWeeklyDay( DateTime clientDate, DateTime oldWeeklyDay, List<DateTime> excludeDays )
    {
        DayOfWeek day = oldWeeklyDay.DayOfWeek;
        DateTime result = clientDate.Date;
        while ( result.DayOfWeek != day || excludeDays.Any( x => x == result.Date ) )
        {
            result = result.AddDays( 1 );
        }
        
        return result;
    }

    private void FillBooking( Booking booking, SaveSchedulerBookingDataCommand request )
    {
        booking.ManualDiscount = request.ManualDiscount;
        booking.HoursCount = request.HoursCount;
        booking.IsWeekly = request.IsWeekly;
        booking.TariffId = request.TariffId;

        booking.Costs = Mapper.Map<List<BookingCost>>( request.Costs );
        booking.Inventories = Mapper.Map<List<BookingInventory>>( request.Inventories );
        booking.Customer = Mapper.Map<BookingCustomer>( request.Customer );

        if ( request.LockerRoomId.HasValue && !request.IsWeekly )
        {
            DateTime bookingLockerRoomStart = booking.Day.AddHours( ( double )booking.StartHour - 0.5 );
            DateTime bookingLockerRoomEnd =
                booking.Day.AddHours( ( double )( booking.StartHour + booking.HoursCount ) + 0.5 );
            booking.BookingLockerRoom = new BookingLockerRoom
            {
                LockerRoomId = request.LockerRoomId.Value,
                Start = bookingLockerRoomStart,
                End = bookingLockerRoomEnd
            };
        }
        else
        {
            booking.BookingLockerRoom = null;
        }

        booking.FieldAmount = booking.Costs.Sum( x => x.Cost );
        booking.InventoryAmount = booking.Inventories.Sum( x => x.Amount );
        booking.TotalAmountBeforeDiscount = booking.FieldAmount + booking.InventoryAmount;
        booking.TotalAmountAfterDiscount = booking.TotalAmountBeforeDiscount - ( request.ManualDiscount ?? 0 ) -
                                           ( booking.PromoDiscount ?? 0 );
        booking.IsConfirmed = true;
        booking.IsDraft = false;
    }

    private string GetWeeklyBookingNumber( string bookingNumber )
    {
        string[] partsNumber = bookingNumber.Split( "/" );
        if ( partsNumber.Length == 1 )
        {
            return $"{bookingNumber}/2";
        }

        int i = Int32.Parse( partsNumber[ 1 ] );

        return $"{partsNumber[ 0 ]}/{i + 1}";
    }
}
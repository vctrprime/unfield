using AutoMapper;
using StadiumEngine.Commands.Schedule;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Entities.Bookings;
using StadiumEngine.Domain.Services.Core.Schedule;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Schedule;

namespace StadiumEngine.Handlers.Handlers.Schedule;

internal sealed class SaveSchedulerBookingDataHandler : BaseCommandHandler<SaveSchedulerBookingDataCommand, SaveSchedulerBookingDataDto>
{
    private readonly ISchedulerBookingQueryService _queryService;
    private readonly ISchedulerBookingCommandService _commandService;

    public SaveSchedulerBookingDataHandler(
        ISchedulerBookingQueryService queryService,
        ISchedulerBookingCommandService commandService,
        IClaimsIdentityService claimsIdentityService,
        IMapper mapper,
        IUnitOfWork unitOfWork ) : base( mapper, claimsIdentityService, unitOfWork )
    {
        _queryService = queryService;
        _commandService = commandService;
    }

    protected override async ValueTask<SaveSchedulerBookingDataDto> HandleCommandAsync(
        SaveSchedulerBookingDataCommand request,
        CancellationToken cancellationToken )
    {
        Booking booking = await _queryService.GetBookingAsync( request.BookingNumber );

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
                    Day = booking.Day.Date,
                    AccessCode = booking.AccessCode,
                    Source = booking.Source,
                    IsLastVersion = true,
                    UserCreatedId = _userId,
                    FieldId = booking.FieldId,
                    PromoDiscount = booking.PromoDiscount,
                    Field = booking.Field,
                    StartHour = booking.StartHour
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
                        Day = request.Day.Date,
                        StartHour = booking.StartHour,
                        Source = booking.Source,
                        IsLastVersion = true,
                        UserCreatedId = _userId,
                        FieldId = booking.FieldId,
                        Field = booking.Field
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
                        Number = GetWeeklyBookingNumber(booking.Number),
                        AccessCode = booking.AccessCode,
                        StartHour = booking.StartHour,
                        Day = request.Day.Date,
                        Source = booking.Source,
                        IsLastVersion = true,
                        UserCreatedId = _userId,
                        FieldId = booking.FieldId,
                        Field = booking.Field
                    };
                    FillBooking( newWeekly, request );
                    await _commandService.AddVersionAsync( newWeekly );
                    
                    booking.IsWeeklyStoppedDate = request.Day.Date.AddDays( -1 );
                    booking.UserModifiedId = _userId;
                    _commandService.UpdateOldVersion( booking );
                }
            }
        }


        return new SaveSchedulerBookingDataDto();
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

        booking.BookingLockerRoom = request.LockerRoomId.HasValue
            ? new BookingLockerRoom
            {
                LockerRoomId = request.LockerRoomId.Value
            }
            : null;

        booking.FieldAmount = booking.Costs.Sum( x => x.Cost );
        booking.InventoryAmount = booking.Inventories.Sum( x => x.Amount );
        booking.TotalAmountBeforeDiscount = booking.FieldAmount + booking.InventoryAmount;
        booking.TotalAmountAfterDiscount = booking.TotalAmountBeforeDiscount - ( request.ManualDiscount ?? 0 ) -
                                           ( booking.PromoDiscount ?? 0 );
        booking.IsConfirmed = true;
    }

    private string GetWeeklyBookingNumber( string bookingNumber )
    {
        string[] partsNumber = bookingNumber.Split( "/" );
        if ( partsNumber.Length == 1 )
        {
            return $"{bookingNumber}/2";
        }

        string[] partsRows = partsNumber[ 1 ].Split( "-" );
        int i = Int32.Parse( partsRows[ 0 ] );
        
        return $"{partsNumber[0]}/{i}";
    }
}
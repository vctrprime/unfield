using AutoMapper;
using StadiumEngine.Commands.BookingForm;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Entities.Bookings;
using StadiumEngine.Domain.Services.Application.BookingForm;
using StadiumEngine.Domain.Services.Infrastructure;
using StadiumEngine.DTO.BookingForm;

namespace StadiumEngine.Handlers.Handlers.BookingForm;

internal sealed class FillBookingDataHandler : BaseCommandHandler<FillBookingDataCommand, FillBookingDataDto>
{
    private readonly IBookingCheckoutQueryService _queryService;
    private readonly IBookingCheckoutCommandService _commandService;
    private readonly ISmsSender _smsSender;

    public FillBookingDataHandler(
        IBookingCheckoutQueryService queryService,
        IBookingCheckoutCommandService commandService,
        ISmsSender smsSender,
        IMapper mapper,
        IUnitOfWork unitOfWork ) : base( mapper, null, unitOfWork )
    {
        _queryService = queryService;
        _commandService = commandService;
        _smsSender = smsSender;
    }

    protected override async ValueTask<FillBookingDataDto> HandleCommandAsync(
        FillBookingDataCommand request,
        CancellationToken cancellationToken )
    {
        Booking booking = await _queryService.GetBookingDraftAsync( request.BookingNumber );

        booking.Amount = request.Amount;
        booking.Discount = request.Discount;
        booking.PromoCode = request.PromoCode;
        booking.HoursCount = request.HoursCount;

        booking.Costs = Mapper.Map<List<BookingCost>>( request.Costs );
        booking.Inventories = Mapper.Map<List<BookingInventory>>( request.Inventories );
        booking.Customer = Mapper.Map<BookingCustomer>( request.Customer );
        
        await _commandService.FillBookingDataAsync( booking );

        await _smsSender.SendBookingAccessCodeAsync( booking, request.Language );

        return new FillBookingDataDto();
    }
}
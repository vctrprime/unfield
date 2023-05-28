using AutoMapper;
using StadiumEngine.Commands.BookingForm;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Entities.BookingForm;
using StadiumEngine.Domain.Services.Facades.BookingForm;
using StadiumEngine.Domain.Services.Infrastructure;
using StadiumEngine.DTO.BookingForm;

namespace StadiumEngine.Handlers.Handlers.BookingForm;

internal sealed class FillBookingDataHandler : BaseCommandHandler<FillBookingDataCommand, FillBookingDataDto>
{
    private readonly IBookingCheckoutQueryFacade _queryFacade;
    private readonly IBookingCheckoutCommandFacade _commandFacade;
    private readonly ISmsSender _smsSender;

    public FillBookingDataHandler(
        IBookingCheckoutQueryFacade queryFacade,
        IBookingCheckoutCommandFacade commandFacade,
        ISmsSender smsSender,
        IMapper mapper,
        IUnitOfWork unitOfWork ) : base( mapper, null, unitOfWork )
    {
        _queryFacade = queryFacade;
        _commandFacade = commandFacade;
        _smsSender = smsSender;
    }

    protected override async ValueTask<FillBookingDataDto> HandleCommandAsync(
        FillBookingDataCommand request,
        CancellationToken cancellationToken )
    {
        Booking booking = await _queryFacade.GetBookingDraftAsync( request.BookingNumber );

        booking.Amount = request.Amount;
        booking.Discount = request.Discount;
        booking.PromoCode = request.PromoCode;
        booking.HoursCount = request.HoursCount;

        booking.Costs = Mapper.Map<List<BookingCost>>( request.Costs );
        booking.Inventories = Mapper.Map<List<BookingInventory>>( request.Inventories );
        booking.Customer = Mapper.Map<BookingCustomer>( request.Customer );
        
        _commandFacade.FillBookingData( booking );

        await _smsSender.SendBookingNotificationAsync( booking );

        return new FillBookingDataDto();
    }
}
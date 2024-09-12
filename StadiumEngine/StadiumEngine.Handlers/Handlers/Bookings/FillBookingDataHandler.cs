using AutoMapper;
using Mediator;
using StadiumEngine.Commands.BookingForm;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Entities.Bookings;
using StadiumEngine.Domain.Services.Core.BookingForm;
using StadiumEngine.Domain.Services.Infrastructure;
using StadiumEngine.DTO.BookingForm;
using StadiumEngine.DTO.Customers;
using StadiumEngine.Handlers.Facades.Bookings;
using StadiumEngine.Handlers.Resolvers.Customers;

namespace StadiumEngine.Handlers.Handlers.Bookings;

internal sealed class FillBookingDataHandler : BaseCommandHandler<FillBookingDataCommand, FillBookingDataDto>
{
    private readonly IBookingCheckoutQueryService _queryService;
    private readonly IBookingCheckoutCommandService _commandService;
    private readonly ISmsSender _smsSender;
    private readonly IBookingAuthorizedCustomerResolver _authorizedCustomerResolver;
    private readonly IConfirmBookingFacade _confirmBookingFacade;

    public FillBookingDataHandler(
        IBookingCheckoutQueryService queryService,
        IBookingCheckoutCommandService commandService,
        ISmsSender smsSender,
        IBookingAuthorizedCustomerResolver authorizedCustomerResolver,
        IConfirmBookingFacade confirmBookingFacade,
        IMapper mapper,
        IUnitOfWork unitOfWork ) : base( mapper, null, unitOfWork )
    {
        _queryService = queryService;
        _commandService = commandService;
        _smsSender = smsSender;
        _authorizedCustomerResolver = authorizedCustomerResolver;
        _confirmBookingFacade = confirmBookingFacade;
    }

    protected override async ValueTask<FillBookingDataDto> HandleCommandAsync(
        FillBookingDataCommand request,
        CancellationToken cancellationToken )
    {
        Booking booking = await _queryService.GetBookingDraftAsync( request.BookingNumber );

        booking.PromoDiscount = request.PromoDiscount;
        booking.HoursCount = request.HoursCount;

        booking.Costs = Mapper.Map<List<BookingCost>>( request.Costs );
        booking.Inventories = Mapper.Map<List<BookingInventory>>( request.Inventories );
        booking.Customer = Mapper.Map<BookingCustomer>( request.Customer );

        booking.FieldAmount = booking.Costs.Sum( x => x.Cost );
        booking.InventoryAmount = booking.Inventories.Sum( x => x.Amount );
        booking.TotalAmountBeforeDiscount = booking.FieldAmount + booking.InventoryAmount;
        booking.TotalAmountAfterDiscount = booking.TotalAmountBeforeDiscount - (request.PromoDiscount ?? 0);

        if ( request.Promo != null )
        {
            booking.Promo = Mapper.Map<BookingPromo>( request.Promo );
        }
        
        await _commandService.FillBookingDataAsync( booking );
        
        AuthorizedCustomerDto? customer = await _authorizedCustomerResolver.ResolveAsync( booking.Source, booking.Field.StadiumId );

        // если пользователь авторизован и совпадает, то не требуем подтверждения, подтверждаем автоматически
        if ( customer != null && customer.PhoneNumber == booking.Customer.PhoneNumber )
        {
            ConfirmBookingDto confirmDto = await _confirmBookingFacade.ConfirmAsync(
                new ConfirmBookingCommand
                {
                    ClientDate = request.ClientDate,
                    BookingNumber = booking.Number,
                    AccessCode = booking.AccessCode,
                    Language = request.Language
                },
                cancellationToken );

            return new FillBookingDataDto
            {
                RedirectUrl = confirmDto.RedirectUrl
            };
        }

        await _smsSender.SendBookingAccessCodeAsync( booking, request.Language );

        return new FillBookingDataDto();
    }
}
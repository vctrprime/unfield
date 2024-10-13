using AutoMapper;
using Mediator;
using Unfield.Commands.BookingForm;
using Unfield.Domain;
using Unfield.Domain.Entities.Bookings;
using Unfield.Domain.Services.Core.BookingForm;
using Unfield.Domain.Services.Infrastructure;
using Unfield.DTO.BookingForm;
using Unfield.DTO.Customers;
using Unfield.Handlers.Facades.Bookings;
using Unfield.Handlers.Resolvers.Customers;

namespace Unfield.Handlers.Handlers.Bookings;

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
                } );

            return new FillBookingDataDto
            {
                RedirectUrl = confirmDto.RedirectUrl
            };
        }

        await _smsSender.SendBookingAccessCodeAsync( booking, request.Language );

        return new FillBookingDataDto();
    }
}
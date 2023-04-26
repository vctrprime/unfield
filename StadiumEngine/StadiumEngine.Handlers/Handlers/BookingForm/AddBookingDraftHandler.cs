using AutoMapper;
using StadiumEngine.Commands.BookingForm;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Entities.BookingForm;
using StadiumEngine.Domain.Services.Facades.BookingForm;
using StadiumEngine.DTO.BookingForm;

namespace StadiumEngine.Handlers.Handlers.BookingForm;

internal sealed class AddBookingDraftHandler : BaseCommandHandler<AddBookingDraftCommand, AddBookingDraftDto>
{
    private readonly IBookingFormCommandFacade _commandFacade;

    public AddBookingDraftHandler(
        IBookingFormCommandFacade commandFacade,
        IMapper mapper,
        IUnitOfWork unitOfWork ) : base(
        mapper,
        null,
        unitOfWork )
    {
        _commandFacade = commandFacade;
    }

    protected override async ValueTask<AddBookingDraftDto> HandleCommandAsync(
        AddBookingDraftCommand request,
        CancellationToken cancellationToken )
    {
        Booking booking = Mapper.Map<Booking>( request );

        await _commandFacade.CreateBookingAsync( booking );

        return new AddBookingDraftDto
        {
            BookingNumber = booking.Number
        };
    }
}
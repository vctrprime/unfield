using AutoMapper;
using StadiumEngine.Commands.BookingForm;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Entities.Bookings;
using StadiumEngine.Domain.Services.Application.BookingForm;
using StadiumEngine.DTO.BookingForm;

namespace StadiumEngine.Handlers.Handlers.BookingForm;

internal sealed class AddBookingDraftHandler : BaseCommandHandler<AddBookingDraftCommand, AddBookingDraftDto>
{
    private readonly IBookingFormCommandService _commandService;

    public AddBookingDraftHandler(
        IBookingFormCommandService commandService,
        IMapper mapper,
        IUnitOfWork unitOfWork ) : base(
        mapper,
        null,
        unitOfWork )
    {
        _commandService = commandService;
    }

    protected override async ValueTask<AddBookingDraftDto> HandleCommandAsync(
        AddBookingDraftCommand request,
        CancellationToken cancellationToken )
    {
        Booking booking = Mapper.Map<Booking>( request );

        await _commandService.CreateBookingAsync( booking );

        return new AddBookingDraftDto
        {
            BookingNumber = booking.Number
        };
    }
}
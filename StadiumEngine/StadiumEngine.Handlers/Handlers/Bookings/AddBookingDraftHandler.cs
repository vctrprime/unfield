using AutoMapper;
using StadiumEngine.Commands.BookingForm;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Entities.Bookings;
using StadiumEngine.Domain.Services.Core.BookingForm;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.BookingForm;

namespace StadiumEngine.Handlers.Handlers.Bookings;

internal sealed class AddBookingDraftHandler : BaseCommandHandler<AddBookingDraftCommand, AddBookingDraftDto>
{
    private readonly IBookingFormCommandService _commandService;

    public AddBookingDraftHandler(
        IBookingFormCommandService commandService,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService,
        IUnitOfWork unitOfWork ) : base(
        mapper,
        claimsIdentityService,
        unitOfWork )
    {
        _commandService = commandService;
    }

    protected override async ValueTask<AddBookingDraftDto> HandleCommandAsync(
        AddBookingDraftCommand request,
        CancellationToken cancellationToken )
    {
        Booking booking = Mapper.Map<Booking>( request );

        if ( _userId != 0 )
        {
            booking.UserCreatedId = _userId;
        }

        await _commandService.CreateBookingAsync( booking, UnitOfWork );

        return new AddBookingDraftDto
        {
            BookingNumber = booking.Number
        };
    }
}
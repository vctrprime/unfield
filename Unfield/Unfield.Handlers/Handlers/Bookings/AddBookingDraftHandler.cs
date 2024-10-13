using AutoMapper;
using Unfield.Commands.BookingForm;
using Unfield.Domain;
using Unfield.Domain.Entities.Bookings;
using Unfield.Domain.Services.Core.BookingForm;
using Unfield.Domain.Services.Identity;
using Unfield.DTO.BookingForm;

namespace Unfield.Handlers.Handlers.Bookings;

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
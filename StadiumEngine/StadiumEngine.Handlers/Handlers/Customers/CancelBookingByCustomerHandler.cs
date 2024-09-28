using AutoMapper;
using StadiumEngine.Commands.Customers;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Customers;
using StadiumEngine.Handlers.Facades.Bookings;

namespace StadiumEngine.Handlers.Handlers.Customers;

internal sealed class CancelBookingByCustomerHandler : BaseCustomerCommandHandler<CancelBookingByCustomerCommand, CancelBookingByCustomerDto>
{
    private readonly ICancelBookingFacade _cancelBookingFacade;

    public CancelBookingByCustomerHandler(
        ICancelBookingFacade cancelBookingFacade,
        ICustomerClaimsIdentityService claimsIdentityService,
        IMapper mapper,
        IUnitOfWork unitOfWork ) : base( mapper, claimsIdentityService, unitOfWork )
    {
        _cancelBookingFacade = cancelBookingFacade;
    }

    protected override async ValueTask<CancelBookingByCustomerDto> HandleCommandAsync(
        CancelBookingByCustomerCommand request,
        CancellationToken cancellationToken )
    {
        await _cancelBookingFacade.CancelBooking( request, null, _customerPhoneNumber );
        return new CancelBookingByCustomerDto();
    }
}
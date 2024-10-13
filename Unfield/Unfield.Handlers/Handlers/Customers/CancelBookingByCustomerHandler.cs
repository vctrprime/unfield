using AutoMapper;
using Unfield.Commands.Customers;
using Unfield.Domain;
using Unfield.Domain.Services.Identity;
using Unfield.DTO.Customers;
using Unfield.Handlers.Facades.Bookings;

namespace Unfield.Handlers.Handlers.Customers;

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
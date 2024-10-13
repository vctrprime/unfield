using AutoMapper;
using Unfield.Commands.Customers;
using Unfield.Domain;
using Unfield.Domain.Services.Core.Customers;
using Unfield.DTO.Customers;

namespace Unfield.Handlers.Handlers.Customers;

internal sealed class ResetCustomerPasswordHandler : BaseCustomerCommandHandler<ResetCustomerPasswordCommand, ResetCustomerPasswordDto>
{
    private readonly ICustomerCommandService _commandService;

    public ResetCustomerPasswordHandler(
        ICustomerCommandService commandService,
        IMapper mapper,
        IUnitOfWork unitOfWork ) : base(
        mapper,
        null,
        unitOfWork )
    {
        _commandService = commandService;
    }

    protected override async ValueTask<ResetCustomerPasswordDto> HandleCommandAsync( ResetCustomerPasswordCommand request,
        CancellationToken cancellationToken )
    {
        await _commandService.ResetPasswordAsync( request.Data.PhoneNumber, request.StadiumToken );
        return new ResetCustomerPasswordDto();
    }
}
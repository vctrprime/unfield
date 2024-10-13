using AutoMapper;
using Unfield.Commands.Customers;
using Unfield.Common;
using Unfield.Common.Exceptions;
using Unfield.Domain;
using Unfield.Domain.Services.Core.Customers;
using Unfield.Domain.Services.Identity;
using Unfield.DTO.Customers;

namespace Unfield.Handlers.Handlers.Customers;

internal sealed class ChangeCustomerPasswordHandler : BaseCustomerCommandHandler<ChangeCustomerPasswordCommand, ChangeCustomerPasswordDto>
{
    private readonly ICustomerCommandService _commandService;

    public ChangeCustomerPasswordHandler(
        ICustomerCommandService commandService,
        IMapper mapper,
        ICustomerClaimsIdentityService claimsIdentityService,
        IUnitOfWork unitOfWork ) : base( mapper, claimsIdentityService, unitOfWork )
    {
        _commandService = commandService;
    }

    protected override async ValueTask<ChangeCustomerPasswordDto> HandleCommandAsync( ChangeCustomerPasswordCommand request,
        CancellationToken cancellationToken )
    {
        if ( request.Data.NewPassword != request.Data.NewPasswordRepeat )
        {
            throw new DomainException( ErrorsKeys.PasswordsNotEqual );
        }

        await _commandService.ChangePasswordAsync( _customerId, request.Data.NewPassword, request.Data.OldPassword );

        return new ChangeCustomerPasswordDto();
    }
}
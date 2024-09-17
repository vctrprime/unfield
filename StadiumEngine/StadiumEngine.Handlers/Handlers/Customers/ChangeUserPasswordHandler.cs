using AutoMapper;
using StadiumEngine.Commands.Customers;
using StadiumEngine.Common;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Services.Core.Customers;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Customers;

namespace StadiumEngine.Handlers.Handlers.Customers;

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
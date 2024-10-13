using AutoMapper;
using Unfield.Commands.Customers;
using Unfield.Domain;
using Unfield.Domain.Services.Core.Customers;
using Unfield.Domain.Services.Identity;
using Unfield.DTO.Customers;

namespace Unfield.Handlers.Handlers.Customers;

internal class ChangeCustomerLanguageHandler : BaseCustomerCommandHandler<ChangeCustomerLanguageCommand, ChangeCustomerLanguageDto>
{
    private readonly ICustomerCommandService _commandService;

    public ChangeCustomerLanguageHandler(
        ICustomerCommandService commandService,
        IMapper mapper,
        ICustomerClaimsIdentityService claimsIdentityService,
        IUnitOfWork unitOfWork ) : base( mapper, claimsIdentityService, unitOfWork )
    {
        _commandService = commandService;
    }


    protected override async ValueTask<ChangeCustomerLanguageDto> HandleCommandAsync( ChangeCustomerLanguageCommand request,
        CancellationToken cancellationToken )
    {
        await _commandService.ChangeLanguageAsync( _customerId, request.Language );
        return new ChangeCustomerLanguageDto();
    }
}
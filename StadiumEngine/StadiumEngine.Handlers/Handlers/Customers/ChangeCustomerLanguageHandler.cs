using AutoMapper;
using StadiumEngine.Commands.Customers;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Services.Core.Customers;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Customers;

namespace StadiumEngine.Handlers.Handlers.Customers;

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
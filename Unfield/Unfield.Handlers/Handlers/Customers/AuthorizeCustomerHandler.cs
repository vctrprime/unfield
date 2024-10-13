using AutoMapper;
using Unfield.Commands.Customers;
using Unfield.Domain;
using Unfield.Domain.Entities.Customers;
using Unfield.Domain.Services.Core.Customers;
using Unfield.DTO.Customers;

namespace Unfield.Handlers.Handlers.Customers;

internal sealed class AuthorizeCustomerHandler : BaseCustomerCommandHandler<AuthorizeCustomerCommand, AuthorizeCustomerDto>
{
    private readonly ICustomerCommandService _commandService;

    public AuthorizeCustomerHandler(
        ICustomerCommandService commandService,
        IMapper mapper,
        IUnitOfWork unitOfWork
    )
        : base( mapper, null, unitOfWork )
    {
        _commandService = commandService;
    }

    protected override async ValueTask<AuthorizeCustomerDto> HandleCommandAsync( AuthorizeCustomerCommand request,
        CancellationToken cancellationToken )
    {
        Customer customer = await _commandService.AuthorizeCustomerAsync(
            request.Data.Login,
            request.StadiumToken,
            request.Data.Password );
        
        AuthorizeCustomerDto customerDto = Mapper.Map<AuthorizeCustomerDto>( customer );
        
        return customerDto;
    }
}
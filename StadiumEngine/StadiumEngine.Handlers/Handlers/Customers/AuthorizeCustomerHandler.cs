using AutoMapper;
using StadiumEngine.Commands.Customers;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Entities.Customers;
using StadiumEngine.Domain.Services.Core.Customers;
using StadiumEngine.DTO.Customers;

namespace StadiumEngine.Handlers.Handlers.Customers;

internal sealed class AuthorizeCustomerHandler : BaseCommandHandler<AuthorizeCustomerCommand, AuthorizeCustomerDto>
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
            request.Login,
            request.StadiumId,
            request.Password );
        
        AuthorizeCustomerDto customerDto = Mapper.Map<AuthorizeCustomerDto>( customer );
        
        return customerDto;
    }
}
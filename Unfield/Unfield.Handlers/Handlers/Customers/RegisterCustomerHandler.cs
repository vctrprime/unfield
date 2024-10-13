using AutoMapper;
using Unfield.Commands.Customers;
using Unfield.Common;
using Unfield.Common.Exceptions;
using Unfield.Domain;
using Unfield.Domain.Entities.Accounts;
using Unfield.Domain.Services.Core.Accounts;
using Unfield.Domain.Services.Core.Customers;
using Unfield.Domain.Services.Models.Customers;
using Unfield.DTO.Customers;

namespace Unfield.Handlers.Handlers.Customers;

internal sealed class RegisterCustomerHandler : BaseCustomerCommandHandler<RegisterCustomerCommand, RegisterCustomerDto>
{
    private readonly ICustomerCommandService _customerCommandService;
    private readonly IStadiumQueryService _stadiumQueryService;

    public RegisterCustomerHandler(
        ICustomerCommandService customerCommandService,
        IStadiumQueryService stadiumQueryService,
        IMapper mapper,
        IUnitOfWork unitOfWork ) : base(
        mapper,
        null,
        unitOfWork )
    {
        _customerCommandService = customerCommandService;
        _stadiumQueryService = stadiumQueryService;
    }

    protected override async ValueTask<RegisterCustomerDto> HandleCommandAsync( RegisterCustomerCommand request,
        CancellationToken cancellationToken )
    {
        Stadium? stadium = await _stadiumQueryService.GetAsync( request.StadiumToken );

        if ( stadium is null )
        {
            throw new DomainException( ErrorsKeys.StadiumNotFound );
        }
        
        CreateCustomerData createCustomerData = Mapper.Map<CreateCustomerData>( request.Data );
        createCustomerData.Stadium = stadium;
        
        await _customerCommandService.RegisterAsync( createCustomerData );

        return new RegisterCustomerDto();
    }
}
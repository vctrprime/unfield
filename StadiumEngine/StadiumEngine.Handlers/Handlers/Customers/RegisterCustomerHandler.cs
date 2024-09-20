using AutoMapper;
using StadiumEngine.Commands.Customers;
using StadiumEngine.Common;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Services.Core.Accounts;
using StadiumEngine.Domain.Services.Core.Customers;
using StadiumEngine.Domain.Services.Models.Customers;
using StadiumEngine.DTO.Customers;

namespace StadiumEngine.Handlers.Handlers.Customers;

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
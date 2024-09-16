using Mediator;
using StadiumEngine.DTO.Customers;

namespace StadiumEngine.Commands.Customers;

public sealed class ChangeCustomerLanguageCommand : BaseCustomerCommand, IRequest<ChangeCustomerLanguageDto>
{
    public string Language { get; set; } = null!;
}
using Mediator;
using Unfield.DTO.Customers;

namespace Unfield.Commands.Customers;

public sealed class ChangeCustomerLanguageCommand : BaseCustomerCommand, IRequest<ChangeCustomerLanguageDto>
{
    public string Language { get; set; } = null!;
}
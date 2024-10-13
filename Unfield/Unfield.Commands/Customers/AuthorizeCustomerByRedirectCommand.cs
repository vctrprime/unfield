using Mediator;
using Unfield.DTO.Customers;

namespace Unfield.Commands.Customers;

public sealed class AuthorizeCustomerByRedirectCommand : BaseCommand, IRequest<AuthorizeCustomerDto>
{
    public string Token { get; set; } = null!;
    public string Language { get; set; } = "ru";
}
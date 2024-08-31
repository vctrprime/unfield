using Mediator;
using StadiumEngine.DTO.Customers;

namespace StadiumEngine.Commands.Customers;

public sealed class AuthorizeCustomerByRedirectCommand : BaseCommand, IRequest<AuthorizeCustomerDto>
{
    public string Token { get; set; } = null!;
    public string Language { get; set; } = "ru";
}
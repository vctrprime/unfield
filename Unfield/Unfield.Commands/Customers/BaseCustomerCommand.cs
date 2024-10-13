using Microsoft.AspNetCore.Mvc;

namespace Unfield.Commands.Customers;

public abstract class BaseCustomerCommand : BaseCommand
{
    [FromHeader( Name = "SE-Stadium-Token" )]
    public string StadiumToken { get; set; } = null!;
}
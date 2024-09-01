using Microsoft.AspNetCore.Mvc;

namespace StadiumEngine.Commands.Customers;

public abstract class BaseCustomerCommand : BaseCommand
{
    [FromHeader(Name = "SE-Stadium-Id")]
    public int StadiumId { get; set; }
}
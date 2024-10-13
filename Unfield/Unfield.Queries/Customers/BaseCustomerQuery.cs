using Microsoft.AspNetCore.Mvc;

namespace Unfield.Queries.Customers;

public abstract class BaseCustomerQuery : BaseQuery
{
    [FromHeader( Name = "SE-Stadium-Token" )]
    public string StadiumToken { get; set; } = null!;
}
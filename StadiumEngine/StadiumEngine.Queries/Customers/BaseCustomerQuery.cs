using Microsoft.AspNetCore.Mvc;

namespace StadiumEngine.Queries.Customers;

public abstract class BaseCustomerQuery : BaseQuery
{
    [FromHeader( Name = "SE-Stadium-Token" )]
    public string StadiumToken { get; set; } = null!;
}
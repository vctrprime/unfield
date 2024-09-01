using Microsoft.AspNetCore.Mvc;

namespace StadiumEngine.Queries.Customers;

public abstract class BaseCustomerQuery : BaseQuery
{
    [FromHeader(Name = "SE-Stadium-Id")]
    public int StadiumId { get; set; }
}
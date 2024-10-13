using Microsoft.AspNetCore.Mvc;

namespace Unfield.Queries;

public abstract class BaseQuery
{
    [FromHeader(Name = "Client-Date")]
    public DateTime ClientDate { get; set; }
}
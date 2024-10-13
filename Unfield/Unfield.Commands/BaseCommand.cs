using Microsoft.AspNetCore.Mvc;

namespace Unfield.Commands;

public abstract class BaseCommand
{
    [FromHeader(Name = "Client-Date")]
    public DateTime ClientDate { get; set; }
}
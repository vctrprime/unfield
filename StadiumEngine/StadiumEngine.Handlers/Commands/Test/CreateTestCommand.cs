using Mediator;
using StadiumEngine.DTO.Test;

namespace StadiumEngine.Handlers.Commands.Test;

public sealed class CreateTestCommand : IRequest<TestDto>
{
    public string Name { get; set; }
    public int RegionId { get; set; }
}
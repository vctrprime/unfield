using Mediator;
using StadiumEngine.Common.Enums.Offers;
using StadiumEngine.DTO.Offers.Fields;

namespace StadiumEngine.Handlers.Commands.Offers.Fields;

public sealed class AddFieldCommand : IRequest<AddFieldDto>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Width { get; set; }
    public decimal Length { get; set; }
    public int? ParentFieldId { get; set; }
    public bool IsActive { get; set; }
    public CoveringType CoveringType { get; set; }
    public List<SportKind> SportKinds { get; set; }
    public List<OffersImagePassed> Images { get; set; } = new List<OffersImagePassed>();
}


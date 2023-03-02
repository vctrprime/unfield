using Mediator;
using StadiumEngine.Common.Enums.Offers;
using StadiumEngine.Common.Models;
using StadiumEngine.DTO.Offers.Fields;

namespace StadiumEngine.Handlers.Commands.Offers.Fields;

public sealed class AddFieldCommand : IRequest<AddFieldDto>
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public decimal Width { get; set; }
    public decimal Length { get; set; }
    public int? ParentFieldId { get; set; }
    public bool IsActive { get; set; }
    public CoveringType CoveringType { get; set; }
    public List<SportKind> SportKinds { get; set; } = new List<SportKind>();
    public List<ImageFile> Images { get; set; } = new List<ImageFile>();
    public int? PriceGroupId { get; set; }
}


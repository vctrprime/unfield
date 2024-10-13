using Mediator;
using Unfield.Common.Enums.Offers;
using Unfield.Common.Models;
using Unfield.DTO.Offers.Fields;

namespace Unfield.Commands.Offers.Fields;

public sealed class AddFieldCommand : BaseCommand, IRequest<AddFieldDto>
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Width { get; set; }
    public decimal Length { get; set; }
    public int? ParentFieldId { get; set; }
    public bool IsActive { get; set; }
    public CoveringType CoveringType { get; set; }
    public List<SportKind> SportKinds { get; set; } = new();
    public List<ImageFile> Images { get; set; } = new();
    public int? PriceGroupId { get; set; }
}
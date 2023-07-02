using Mediator;
using StadiumEngine.Common.Enums.Offers;
using StadiumEngine.Common.Models;
using StadiumEngine.DTO.Offers.Fields;

namespace StadiumEngine.Commands.Offers.Fields;

public sealed class UpdateFieldCommand : BaseCommand, IRequest<UpdateFieldDto>
{
    public int Id { get; set; }
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
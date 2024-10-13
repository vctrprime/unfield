using Mediator;
using Unfield.Common.Enums.Offers;
using Unfield.Common.Models;
using Unfield.DTO.Offers.Inventories;

namespace Unfield.Commands.Offers.Inventories;

public sealed class AddInventoryCommand : BaseCommand, IRequest<AddInventoryDto>
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public decimal Quantity { get; set; }
    public bool IsActive { get; set; }
    public Currency Currency { get; set; }
    public List<SportKind> SportKinds { get; set; } = new();
    public List<ImageFile> Images { get; set; } = new();
}
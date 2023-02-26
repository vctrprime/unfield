using Mediator;
using StadiumEngine.Common.Enums.Offers;
using StadiumEngine.Common.Models;
using StadiumEngine.DTO.Offers.Inventories;

namespace StadiumEngine.Handlers.Commands.Offers.Inventories;

public sealed class UpdateInventoryCommand : IRequest<UpdateInventoryDto>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public decimal Quantity { get; set; }
    public bool IsActive { get; set; }
    public Currency Currency { get; set; }
    public List<SportKind> SportKinds { get; set; } = new List<SportKind>();
    public List<ImageFile> Images { get; set; } = new List<ImageFile>();
}
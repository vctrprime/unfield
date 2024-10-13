using Mediator;
using Unfield.DTO.Offers.Inventories;

namespace Unfield.Commands.Offers.Inventories;

public sealed class DeleteInventoryCommand : BaseCommand, IRequest<DeleteInventoryDto>
{
    public int InventoryId { get; set; }
}
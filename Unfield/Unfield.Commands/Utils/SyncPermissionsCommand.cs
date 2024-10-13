using Mediator;
using Unfield.DTO.Utils;

namespace Unfield.Commands.Utils;

public sealed class SyncPermissionsCommand : BaseCommand, IRequest<SyncPermissionsDto>
{
}
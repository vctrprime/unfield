using Unfield.Domain.Entities.Offers;

namespace Unfield.Services.Handlers.Offers;

internal interface IFieldPriceGroupHandler
{
    Task HandleAsync( Field field, int? userId );
}
using StadiumEngine.Domain.Entities.Offers;

namespace StadiumEngine.Services.Handlers.Offers;

internal interface IFieldPriceGroupHandler
{
    Task HandleAsync( Field field, int? userId );
}
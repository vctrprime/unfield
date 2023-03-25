using StadiumEngine.Domain.Entities.Offers;

namespace StadiumEngine.Services.Handlers.Offers;

internal interface IFieldPriceGroupHandler
{
    Task Handle( Field field, int? userId );
}
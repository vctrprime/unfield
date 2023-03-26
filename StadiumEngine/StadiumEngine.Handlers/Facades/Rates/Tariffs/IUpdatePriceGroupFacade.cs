using StadiumEngine.Domain;
using StadiumEngine.DTO.Rates.Tariffs;
using StadiumEngine.Commands.Rates.Tariffs;
using StadiumEngine.Domain.Entities.Rates;

namespace StadiumEngine.Handlers.Facades.Rates.Tariffs;

internal interface IUpdateTariffFacade
{
    Task<UpdateTariffDto> UpdateAsync( UpdateTariffCommand request, List<PromoCode> promoCodes, int stadiumId, int userId, IUnitOfWork unitOfWork );
}
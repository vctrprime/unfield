using Unfield.Domain;
using Unfield.DTO.Rates.Tariffs;
using Unfield.Commands.Rates.Tariffs;
using Unfield.Domain.Entities.Rates;

namespace Unfield.Handlers.Facades.Rates.Tariffs;

internal interface IUpdateTariffFacade
{
    Task<UpdateTariffDto> UpdateAsync( UpdateTariffCommand request, List<PromoCode> promoCodes, int stadiumId,
        int userId );
}
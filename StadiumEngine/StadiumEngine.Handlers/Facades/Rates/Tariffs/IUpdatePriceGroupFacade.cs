using StadiumEngine.Domain;
using StadiumEngine.DTO.Rates.Tariffs;
using StadiumEngine.Commands.Rates.Tariffs;

namespace StadiumEngine.Handlers.Facades.Rates.Tariffs;

internal interface IUpdateTariffFacade
{
    Task<UpdateTariffDto> Update( UpdateTariffCommand request, int stadiumId, int userId, IUnitOfWork unitOfWork );
}
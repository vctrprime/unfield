using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Domain.Services.Facades.Accounts;

public interface ILegalCommandFacade
{
    Task<string> AddLegalAsync( Legal legal, User superuser );
}
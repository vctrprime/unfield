using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Domain.Services.Core.Accounts;

public interface ILegalCommandService
{
    Task<string> AddLegalAsync( Legal legal, User superuser );
}
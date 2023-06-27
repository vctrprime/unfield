using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Domain.Services.Application.Accounts;

public interface ILegalCommandService
{
    Task<string> AddLegalAsync( Legal legal, User superuser );
}
using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Services.Builders.Utils;

internal interface INewLegalBuilder
{
    Task<string> BuildAsync( Legal legal, User superuser );
}
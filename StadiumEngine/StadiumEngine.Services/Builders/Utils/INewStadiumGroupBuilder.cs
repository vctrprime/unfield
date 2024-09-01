using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Services.Builders.Utils;

internal interface INewStadiumGroupBuilder
{
    Task<string> BuildAsync( StadiumGroup stadiumGroup, User superuser );
}
using Unfield.Domain.Entities.Accounts;

namespace Unfield.Services.Builders.Utils;

internal interface INewStadiumGroupBuilder
{
    Task<string> BuildAsync( StadiumGroup stadiumGroup, User superuser );
}
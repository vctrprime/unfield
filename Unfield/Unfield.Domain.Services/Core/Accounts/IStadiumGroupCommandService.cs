using Unfield.Domain.Entities.Accounts;

namespace Unfield.Domain.Services.Core.Accounts;

public interface IStadiumGroupCommandService
{
    Task<string> AddStadiumGroupAsync( StadiumGroup stadiumGroup, User superuser );
}
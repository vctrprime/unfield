using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Domain.Services.Core.Accounts;

public interface IStadiumGroupCommandService
{
    Task<string> AddStadiumGroupAsync( StadiumGroup stadiumGroup, User superuser );
}
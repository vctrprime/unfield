using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Repositories.Accounts;
using StadiumEngine.Domain.Services.Core.Accounts;
using StadiumEngine.Services.Facades.Accounts;
using StadiumEngine.Services.Builders.Utils;

namespace StadiumEngine.Services.Core.Accounts;

internal class StadiumGroupCommandService : IStadiumGroupCommandService
{
    private readonly IStadiumGroupRepository _stadiumGroupRepository;
    private readonly INewStadiumGroupBuilder _newStadiumGroupBuilder;
    private readonly IUserServiceFacade _userServiceFacade;

    public StadiumGroupCommandService(
        IUserServiceFacade userServiceFacade,
        IStadiumGroupRepository stadiumGroupRepository,
        INewStadiumGroupBuilder newStadiumGroupBuilder )
    {
        _userServiceFacade = userServiceFacade;
        _stadiumGroupRepository = stadiumGroupRepository;
        _newStadiumGroupBuilder = newStadiumGroupBuilder;
    }

    public async Task<string> AddStadiumGroupAsync( StadiumGroup stadiumGroup, User superuser )
    {
        if ( !stadiumGroup.Stadiums.Any() )
        {
            throw new DomainException( "Передан пустой список объектов для добавления!" );
        }

        superuser.PhoneNumber = _userServiceFacade.CheckPhoneNumber( superuser.PhoneNumber );

        string password = await _newStadiumGroupBuilder.BuildAsync( stadiumGroup, superuser );

        _stadiumGroupRepository.Add( stadiumGroup );

        return password;
    }
}
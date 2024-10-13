using Unfield.Common.Exceptions;
using Unfield.Domain.Entities.Accounts;
using Unfield.Domain.Repositories.Accounts;
using Unfield.Domain.Services.Core.Accounts;
using Unfield.Services.Facades.Accounts;
using Unfield.Services.Builders.Utils;

namespace Unfield.Services.Core.Accounts;

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
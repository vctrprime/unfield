using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Repositories.Accounts;
using StadiumEngine.Domain.Services.Facades.Accounts;
using StadiumEngine.Services.Builders.Utils;
using StadiumEngine.Services.Facades.Services.Accounts;

namespace StadiumEngine.Services.Facades.Accounts;

internal class LegalCommandFacade : ILegalCommandFacade
{
    private readonly ILegalRepository _legalRepository;
    private readonly INewLegalBuilder _newLegalBuilder;
    private readonly IUserServiceFacade _userServiceFacade;

    public LegalCommandFacade(
        IUserServiceFacade userServiceFacade,
        ILegalRepository legalRepository,
        INewLegalBuilder newLegalBuilder )
    {
        _userServiceFacade = userServiceFacade;
        _legalRepository = legalRepository;
        _newLegalBuilder = newLegalBuilder;
    }

    public async Task<string> AddLegalAsync( Legal legal, User superuser )
    {
        if ( !legal.Stadiums.Any() )
        {
            throw new DomainException( "Передан пустой список объектов для добавления!" );
        }

        superuser.PhoneNumber = _userServiceFacade.CheckPhoneNumber( superuser.PhoneNumber );

        string password = await _newLegalBuilder.BuildAsync( legal, superuser );

        _legalRepository.Add( legal );

        return password;
    }
}
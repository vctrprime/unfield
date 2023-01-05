using AutoMapper;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Repositories.Accounts;
using StadiumEngine.Domain.Services;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Accounts;
using StadiumEngine.Handlers.Queries.Accounts;

namespace StadiumEngine.Handlers.Handlers.Accounts;

internal sealed class GetUserStadiumsHandler :  BaseRequestHandler<GetUserStadiumsQuery, List<UserStadiumDto>>
{
    private readonly IStadiumRepository _stadiumRepository;
    private readonly IUserRepository _userRepository;

    public GetUserStadiumsHandler(IMapper mapper, IClaimsIdentityService claimsIdentityService, IUnitOfWork unitOfWork, 
        IStadiumRepository stadiumRepository, IUserRepository userRepository) : base(mapper, claimsIdentityService, unitOfWork)
    {
        _stadiumRepository = stadiumRepository;
        _userRepository = userRepository;
    }

    public override async ValueTask<List<UserStadiumDto>> Handle(GetUserStadiumsQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.Get(_userId);

        List<Stadium> stadiums = new List<Stadium>();
        
        switch (user)
        {
            case { IsSuperuser: true, Role: null }:
                stadiums = await _stadiumRepository.GetForLegal(user.LegalId);
                break;
            case { Role: { } }:
                stadiums = await _stadiumRepository.GetForRole(user.Role.Id);
                break;
        }
        
        var stadiumsDto = Mapper.Map<List<UserStadiumDto>>(stadiums);
        
        stadiumsDto.First(s => s.Id == _currentStadiumId).IsCurrent = true;

        return stadiumsDto;
    }
}
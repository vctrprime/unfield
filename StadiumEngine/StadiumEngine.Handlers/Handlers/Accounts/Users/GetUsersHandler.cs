using AutoMapper;
using StadiumEngine.Domain.Repositories.Accounts;
using StadiumEngine.Domain.Services;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Accounts;
using StadiumEngine.DTO.Accounts.Users;
using StadiumEngine.Handlers.Queries.Accounts;
using StadiumEngine.Handlers.Queries.Accounts.Users;

namespace StadiumEngine.Handlers.Handlers.Accounts.Users;

internal sealed class GetUsersHandler : BaseRequestHandler<GetUsersQuery, List<UserDto>>
{
    private readonly IUserRepository _repository;

    public GetUsersHandler(IMapper mapper, IClaimsIdentityService claimsIdentityService, IUnitOfWork unitOfWork, IUserRepository repository) : base(mapper, claimsIdentityService, unitOfWork)
    {
        _repository = repository;
    }

    public override async ValueTask<List<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _repository.GetAll(_legalId);

        var usersDto = Mapper.Map<List<UserDto>>(users);
        
        return usersDto;
    }
}
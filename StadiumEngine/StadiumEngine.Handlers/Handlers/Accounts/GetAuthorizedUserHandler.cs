using AutoMapper;
using StadiumEngine.Domain.Repositories.Accounts;
using StadiumEngine.Domain.Services;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Accounts;
using StadiumEngine.Handlers.Queries.Accounts;

namespace StadiumEngine.Handlers.Handlers.Accounts;

internal sealed class GetAuthorizedUserHandler : BaseRequestHandler<GetAuthorizedUserQuery, AuthorizedUserDto>
{
    private readonly IUserRepository _repository;
    public GetAuthorizedUserHandler(IMapper mapper, IClaimsIdentityService claimsIdentityService, IUnitOfWork unitOfWork,
        IUserRepository repository) : base(mapper, claimsIdentityService, unitOfWork)
    {
        _repository = repository;
    }

    public override async ValueTask<AuthorizedUserDto> Handle(GetAuthorizedUserQuery request, CancellationToken cancellationToken)
    {
        var userId = ClaimsIdentityService.GetUserId();
        var user = await _repository.Get(userId);

        var userDto = Mapper.Map<AuthorizedUserDto>(user);

        return userDto;
    }
}
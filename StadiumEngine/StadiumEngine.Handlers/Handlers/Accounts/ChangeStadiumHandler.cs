using System.Security.Claims;
using AutoMapper;
using StadiumEngine.Common;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Repositories.Accounts;
using StadiumEngine.Domain.Services;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Accounts;
using StadiumEngine.Handlers.Commands.Accounts;

namespace StadiumEngine.Handlers.Handlers.Accounts;

internal sealed class ChangeStadiumHandler : BaseRequestHandler<ChangeStadiumCommand, AuthorizeUserDto?>
{
    private readonly IUserRepository _repository;

    public ChangeStadiumHandler(IMapper mapper, IClaimsIdentityService claimsIdentityService, IUnitOfWork unitOfWork, IUserRepository repository) : base(mapper, claimsIdentityService, unitOfWork)
    {
        _repository = repository;
    }

    public override async ValueTask<AuthorizeUserDto?> Handle(ChangeStadiumCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.Get(_userId);

        if (user == null) return null;
        
        if ((user.Role == null 
             && user.Legal.Stadiums.FirstOrDefault(s => s.Id == request.StadiumId) == null)
            ||
            (user.Role != null 
             && user.Role.RoleStadiums.Select(s => s.Stadium).FirstOrDefault(s => s.Id == request.StadiumId) == null)) throw new DomainException(ErrorsKeys.Forbidden);
        
        var userDto = Mapper.Map<AuthorizeUserDto>(user);
        var stadiumClaim = userDto.Claims.FirstOrDefault(s => s.Type == "stadiumId");
        
        if (stadiumClaim == null) return userDto;
        
        userDto.Claims.Remove(stadiumClaim);
        userDto.Claims.Add(new Claim("stadiumId", request.StadiumId.ToString()));
        
        return userDto;
    }
    
    
}
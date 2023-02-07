using System.Security.Claims;
using AutoMapper;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Repositories.Accounts;
using StadiumEngine.Domain.Services;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Accounts;
using StadiumEngine.DTO.Accounts.Users;
using StadiumEngine.Handlers.Commands.Admin;

namespace StadiumEngine.Handlers.Handlers.Admin;

internal sealed class ChangeLegalHandler : BaseRequestHandler<ChangeLegalCommand, AuthorizeUserDto?>
{
    private readonly IUserRepository _repository;

    public ChangeLegalHandler(IMapper mapper, IClaimsIdentityService claimsIdentityService, IUnitOfWork unitOfWork, IUserRepository repository) : base(mapper, claimsIdentityService, unitOfWork)
    {
        _repository = repository;
    }

    public override async ValueTask<AuthorizeUserDto?> Handle(ChangeLegalCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.Get(_userId);
        
        if (user == null) return null;

        if (user.LegalId != request.LegalId)
        {
            user.LegalId = request.LegalId;
            _repository.Update(user);
            await UnitOfWork.SaveChanges();
            
            user = await _repository.Get(_userId);
        }
        
        var userDto = Mapper.Map<AuthorizeUserDto>(user);
        
        return userDto;
    }
    
}
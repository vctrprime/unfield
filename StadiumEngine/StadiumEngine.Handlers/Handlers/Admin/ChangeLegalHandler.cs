using AutoMapper;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Services;
using StadiumEngine.Domain.Services.Facades.Accounts;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Accounts.Users;
using StadiumEngine.Handlers.Commands.Admin;

namespace StadiumEngine.Handlers.Handlers.Admin;

internal sealed class ChangeLegalHandler : BaseRequestHandler<ChangeLegalCommand, AuthorizeUserDto?>
{
    private readonly IUserFacade _userFacade;

    public ChangeLegalHandler(
        IUserFacade userFacade,
        IMapper mapper, 
        IClaimsIdentityService claimsIdentityService, 
        IUnitOfWork unitOfWork) : base(mapper, claimsIdentityService, unitOfWork)
    {
        _userFacade = userFacade;
    }

    public override async ValueTask<AuthorizeUserDto?> Handle(ChangeLegalCommand request, CancellationToken cancellationToken)
    {
        await _userFacade.ChangeLegal(_userId, request.LegalId);
        
        await UnitOfWork.SaveChanges();
        
        var user = await _userFacade.GetUser(_userId);
        
        var userDto = Mapper.Map<AuthorizeUserDto>(user);
        
        return userDto;
    }
    
}
using AutoMapper;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Services;
using StadiumEngine.Domain.Services.Facades.Accounts;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Accounts.Users;
using StadiumEngine.Handlers.Commands.Admin;

namespace StadiumEngine.Handlers.Handlers.Admin;

internal sealed class ChangeLegalHandler : BaseCommandHandler<ChangeLegalCommand, AuthorizeUserDto?>
{
    private readonly IUserFacade _userFacade;

    public ChangeLegalHandler(
        IUserFacade userFacade,
        IMapper mapper, 
        IClaimsIdentityService claimsIdentityService, 
        IUnitOfWork unitOfWork) : base(mapper, claimsIdentityService, unitOfWork, false)
    {
        _userFacade = userFacade;
    }

    protected override async ValueTask<AuthorizeUserDto?> HandleCommand(ChangeLegalCommand request, CancellationToken cancellationToken)
    {
        await _userFacade.ChangeLegal(_userId, request.LegalId);
        
        await UnitOfWork.SaveChanges();
        
        var user = await _userFacade.GetUser(_userId);
        
        var userDto = Mapper.Map<AuthorizeUserDto>(user);
        
        return userDto;
    }
    
}
using AutoMapper;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Services;
using StadiumEngine.Domain.Services.Facades.Accounts;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Accounts.Users;
using StadiumEngine.Handlers.Commands.Accounts.Users;

namespace StadiumEngine.Handlers.Handlers.Accounts.Users;

internal class ChangeUserLanguageHandler : BaseRequestHandler<ChangeUserLanguageCommand, ChangeUserLanguageDto>
{
    private readonly IUserFacade _userFacade;

    public ChangeUserLanguageHandler(
        IUserFacade userFacade,
        IMapper mapper, 
        IClaimsIdentityService claimsIdentityService, 
        IUnitOfWork unitOfWork) : base(mapper, claimsIdentityService, unitOfWork)
    {
        _userFacade = userFacade;
    }


    public override async ValueTask<ChangeUserLanguageDto> Handle(ChangeUserLanguageCommand request, CancellationToken cancellationToken)
    {
        await _userFacade.ChangeLanguage(_userId, request.Language);
        await UnitOfWork.SaveChanges();

        return new ChangeUserLanguageDto();
    }
    
}
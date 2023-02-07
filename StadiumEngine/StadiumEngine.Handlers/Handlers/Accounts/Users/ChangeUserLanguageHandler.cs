using AutoMapper;
using StadiumEngine.Common;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Repositories.Accounts;
using StadiumEngine.Domain.Services;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Accounts;
using StadiumEngine.DTO.Accounts.Users;
using StadiumEngine.Handlers.Commands.Accounts;
using StadiumEngine.Handlers.Commands.Accounts.Users;

namespace StadiumEngine.Handlers.Handlers.Accounts.Users;

internal class ChangeUserLanguageHandler : BaseRequestHandler<ChangeUserLanguageCommand, ChangeUserLanguageDto>
{
    private readonly IUserRepository _repository;

    public ChangeUserLanguageHandler(IMapper mapper, 
        IClaimsIdentityService? claimsIdentityService, 
        IUnitOfWork unitOfWork, IUserRepository repository) : base(mapper, claimsIdentityService, unitOfWork)
    {
        _repository = repository;
    }


    public override async ValueTask<ChangeUserLanguageDto> Handle(ChangeUserLanguageCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.Get(_userId);

        if (user == null) throw new DomainException(ErrorsKeys.UserNotFound);
        
        user.Language = request.Language;
        user.UserModifiedId = _userId;
        
        _repository.Update(user);
        await UnitOfWork.SaveChanges();

        return new ChangeUserLanguageDto();
    }
}
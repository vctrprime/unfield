using AutoMapper;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Repositories.Accounts;
using StadiumEngine.Domain.Services;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Accounts;
using StadiumEngine.Handlers.Commands.Accounts;

namespace StadiumEngine.Handlers.Handlers.Accounts;

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

        if (user == null) throw new DomainException("Указанный пользователь не найден!");
        
        user.Language = request.Language;
        user.UserModifiedId = _userId;
        
        _repository.Update(user);
        await UnitOfWork.SaveChanges();

        return new ChangeUserLanguageDto();
    }
}
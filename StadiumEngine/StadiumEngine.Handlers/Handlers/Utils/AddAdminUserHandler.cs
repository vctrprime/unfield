using AutoMapper;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Services;
using StadiumEngine.Domain.Services.Facades.Accounts;
using StadiumEngine.Domain.Services.Infrastructure;
using StadiumEngine.DTO.Utils;
using StadiumEngine.Handlers.Commands.Utils;

namespace StadiumEngine.Handlers.Handlers.Utils;

internal sealed class AddAdminUserHandler : BaseCommandHandler<AddAdminUserCommand, AddAdminUserDto>
{
    private readonly IUserCommandFacade _userFacade;
    private readonly ISmsSender _smsSender;

    public AddAdminUserHandler(
        IUserCommandFacade userFacade,
        ISmsSender smsSender,
        IMapper mapper,
        IUnitOfWork unitOfWork ) : base(
        mapper,
        null,
        unitOfWork,
        false )
    {
        _userFacade = userFacade;
        _smsSender = smsSender;
    }

    protected override async ValueTask<AddAdminUserDto> HandleCommand( AddAdminUserCommand request,
        CancellationToken cancellationToken )
    {
        var user = Mapper.Map<User>( request );

        var password = await _userFacade.AddUser( user, true );
        await UnitOfWork.SaveChanges();

        await _smsSender.SendPassword( user.PhoneNumber, password, user.Language );

        return new AddAdminUserDto();
    }
}
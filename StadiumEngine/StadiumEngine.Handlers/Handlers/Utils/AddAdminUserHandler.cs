using AutoMapper;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Services.Facades.Accounts;
using StadiumEngine.Domain.Services.Infrastructure;
using StadiumEngine.DTO.Utils;
using StadiumEngine.Commands.Utils;

namespace StadiumEngine.Handlers.Handlers.Utils;

internal sealed class AddAdminUserHandler : BaseCommandHandler<AddAdminUserCommand, AddAdminUserDto>
{
    private readonly ISmsSender _smsSender;
    private readonly IUserCommandFacade _userFacade;

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

    protected override async ValueTask<AddAdminUserDto> HandleCommandAsync( AddAdminUserCommand request,
        CancellationToken cancellationToken )
    {
        User? user = Mapper.Map<User>( request );

        string password = await _userFacade.AddUserAsync( user, true );
        await UnitOfWork.SaveChangesAsync();

        await _smsSender.SendPasswordAsync( user.PhoneNumber, password, user.Language );

        return new AddAdminUserDto();
    }
}
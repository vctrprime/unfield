using AutoMapper;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Services.Core.Accounts;
using StadiumEngine.Domain.Services.Infrastructure;
using StadiumEngine.DTO.Utils;
using StadiumEngine.Commands.Utils;

namespace StadiumEngine.Handlers.Handlers.Utils;

internal sealed class AddLegalHandler : BaseCommandHandler<AddLegalCommand, AddLegalDto>
{
    private readonly ILegalCommandService _commandService;
    private readonly ISmsSender _smsSender;

    public AddLegalHandler(
        ILegalCommandService commandService,
        ISmsSender smsSender,
        IMapper mapper,
        IUnitOfWork unitOfWork ) : base(
        mapper,
        null,
        unitOfWork,
        false )
    {
        _commandService = commandService;
        _smsSender = smsSender;
    }

    protected override async ValueTask<AddLegalDto> HandleCommandAsync( AddLegalCommand request,
        CancellationToken cancellationToken )
    {
        Legal? legal = Mapper.Map<Legal>( request );
        User? superuser = Mapper.Map<User>( request.Superuser );

        superuser.Language = request.Language;

        string password = await _commandService.AddLegalAsync( legal, superuser );

        await UnitOfWork.SaveChangesAsync();

        await _smsSender.SendPasswordAsync(
            superuser.PhoneNumber,
            password,
            superuser.Language );

        AddLegalDto? legalDto = Mapper.Map<AddLegalDto>( legal );

        return legalDto;
    }
}
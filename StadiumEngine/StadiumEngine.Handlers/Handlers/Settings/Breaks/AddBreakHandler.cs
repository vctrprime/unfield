using AutoMapper;
using StadiumEngine.Commands.Settings.Breaks;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Entities.Settings;
using StadiumEngine.Domain.Services.Application.Settings;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Settings.Breaks;

namespace StadiumEngine.Handlers.Handlers.Settings.Breaks;

internal sealed class AddBreakHandler : BaseCommandHandler<AddBreakCommand, AddBreakDto>
{
    private readonly IBreakCommandService _commandService;

    public AddBreakHandler(
        IBreakCommandService commandService,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService,
        IUnitOfWork unitOfWork ) : base( mapper, claimsIdentityService, unitOfWork )
    {
        _commandService = commandService;
    }

    protected override async ValueTask<AddBreakDto> HandleCommandAsync( AddBreakCommand request,
        CancellationToken cancellationToken )
    {
        Break @break = Mapper.Map<Break>( request );
        @break.StadiumId = _currentStadiumId;
        @break.UserCreatedId = _userId;
        foreach (BreakField breakField in @break.BreakFields)
        {
            breakField.UserCreatedId = _userId;
        }

        _commandService.AddBreak( @break );
        
        return await Task.Run( () => new AddBreakDto(), cancellationToken );
    }
}
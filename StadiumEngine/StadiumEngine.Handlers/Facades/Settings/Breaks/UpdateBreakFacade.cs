using StadiumEngine.Commands.Settings.Breaks;
using StadiumEngine.Common;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Common.Static;
using StadiumEngine.Domain.Entities.Settings;
using StadiumEngine.Domain.Services.Facades.Settings;
using StadiumEngine.DTO.Settings.Breaks;

namespace StadiumEngine.Handlers.Facades.Settings.Breaks;

internal class UpdateBreakFacade : IUpdateBreakFacade
{
    private readonly IBreakQueryFacade _queryFacade;
    private readonly IBreakCommandFacade _commandFacade;

    public UpdateBreakFacade( IBreakQueryFacade queryFacade, IBreakCommandFacade commandFacade )
    {
        _queryFacade = queryFacade;
        _commandFacade = commandFacade;
    }

    public async Task<UpdateBreakDto> UpdateAsync( UpdateBreakCommand request, int stadiumId, int userId )
    {
        Break? @break = await _queryFacade.GetByBreakIdAsync( request.Id, stadiumId );

        if ( @break == null )
        {
            throw new DomainException( ErrorsKeys.BreakNotFound );
        }

        @break.DateStart = request.DateStart;
        @break.DateEnd = request.DateEnd;
        @break.IsActive = request.IsActive;
        @break.UserModifiedId = userId;
        @break.Name = request.Name;
        @break.Description = request.Description;
        @break.StartHour = TimePointParser.Parse( request.StartHour );
        @break.EndHour = TimePointParser.Parse( request.EndHour );
        
        _commandFacade.UpdateBreak( @break, request.SelectedFields );

        return new UpdateBreakDto();
    }
}
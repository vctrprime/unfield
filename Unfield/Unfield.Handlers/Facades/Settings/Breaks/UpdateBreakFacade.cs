using Unfield.Commands.Settings.Breaks;
using Unfield.Common;
using Unfield.Common.Exceptions;
using Unfield.Common.Static;
using Unfield.Domain.Entities.Settings;
using Unfield.Domain.Services.Core.Settings;
using Unfield.DTO.Settings.Breaks;

namespace Unfield.Handlers.Facades.Settings.Breaks;

internal class UpdateBreakFacade : IUpdateBreakFacade
{
    private readonly IBreakQueryService _queryService;
    private readonly IBreakCommandService _commandService;

    public UpdateBreakFacade( IBreakQueryService queryService, IBreakCommandService commandService )
    {
        _queryService = queryService;
        _commandService = commandService;
    }

    public async Task<UpdateBreakDto> UpdateAsync( UpdateBreakCommand request, int stadiumId, int userId )
    {
        Break? @break = await _queryService.GetByBreakIdAsync( request.Id, stadiumId );

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
        @break.StartHour = request.StartHour;
        @break.EndHour = request.EndHour;
        
        _commandService.UpdateBreak( @break, request.SelectedFields );

        return new UpdateBreakDto();
    }
}
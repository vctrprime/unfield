using StadiumEngine.Common;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Entities.Settings;
using StadiumEngine.Domain.Repositories.Settings;
using StadiumEngine.Domain.Services.Facades.Settings;
using StadiumEngine.Services.Validators.Settings;

namespace StadiumEngine.Services.Facades.Settings;

internal class BreakCommandFacade : IBreakCommandFacade
{
    private readonly IBreakRepository _breakRepository;
    private readonly IBreakFieldRepository _breakFieldRepository;
    private readonly IBreakValidator _validator;

    public BreakCommandFacade(
        IBreakRepository breakRepository,
        IBreakFieldRepository breakFieldRepository,
        IBreakValidator validator )
    {
        _breakRepository = breakRepository;
        _breakFieldRepository = breakFieldRepository;
        _validator = validator;
    }

    public void AddBreak( Break @break )
    {
        _validator.Validate( @break );

        _breakRepository.Add( @break );
    }

    public void UpdateBreak( Break @break, List<int> selectedFields )
    {
        _validator.Validate( @break );

        _breakRepository.Update( @break );

        _breakFieldRepository.Remove( @break.BreakFields.Where( x => !selectedFields.Contains( x.FieldId ) ) );
        _breakFieldRepository.Add(
            selectedFields.Where( x => !@break.BreakFields.Select( bf => bf.FieldId ).Contains( x ) ).Select(
                x => new BreakField
                {
                    BreakId = @break.Id,
                    FieldId = x,
                    UserCreatedId = @break.UserCreatedId
                } ) );
    }

    public async Task DeleteBreakAsync( int breakId, int stadiumId )
    {
        Break? @break = await _breakRepository.GetAsync( breakId, stadiumId );

        if ( @break == null )
        {
            throw new DomainException( ErrorsKeys.BreakNotFound );
        }
        
        _breakFieldRepository.Remove( @break.BreakFields );
        _breakRepository.Remove( @break );
    }
}
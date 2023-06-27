using StadiumEngine.Common;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Entities.Settings;

namespace StadiumEngine.Services.Validators.Settings;

internal class BreakValidator : IBreakValidator
{
    public void Validate( Break @break )
    {
        if ( @break.StartHour >= @break.EndHour ||
             @break.DateStart >= @break.DateEnd )
        {
            throw new DomainException( ErrorsKeys.InvalidBreakPeriod );
        }
    }
}
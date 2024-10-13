using Unfield.Common;
using Unfield.Common.Exceptions;
using Unfield.Domain.Entities.Settings;

namespace Unfield.Services.Validators.Settings;

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
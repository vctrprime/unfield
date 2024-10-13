using Unfield.Domain.Entities.Settings;

namespace Unfield.Services.Validators.Settings;

internal interface IBreakValidator
{
    void Validate( Break @break );
}
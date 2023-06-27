using StadiumEngine.Domain.Entities.Settings;

namespace StadiumEngine.Services.Validators.Settings;

internal interface IBreakValidator
{
    void Validate( Break @break );
}
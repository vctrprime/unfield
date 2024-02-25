using StadiumEngine.Domain.Entities.Offers;

namespace StadiumEngine.Domain.Entities.Settings;

public class BreakField : BaseUserEntity
{
    public int BreakId { get; set; }
    public virtual Break Break { get; set; }
    
    public int FieldId { get; set; }
    public virtual Field Field { get; set; }
}
using Unfield.Domain.Entities.Accounts;

namespace Unfield.Domain.Entities.Settings;

public class MainSettings : BaseUserEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int StadiumId { get; set; }
    public virtual Stadium Stadium { get; set; }
    public int OpenTime { get; set; }
    public int CloseTime { get; set; }
}
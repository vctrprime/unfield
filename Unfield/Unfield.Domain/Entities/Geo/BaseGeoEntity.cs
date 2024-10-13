namespace Unfield.Domain.Entities.Geo;

public class BaseGeoEntity : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string ShortName { get; set; }
}
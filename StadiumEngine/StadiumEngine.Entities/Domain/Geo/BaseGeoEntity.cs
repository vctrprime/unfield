using System.ComponentModel.DataAnnotations.Schema;

namespace StadiumEngine.Entities.Domain.Geo;

public class BaseGeoEntity : BaseEntity
{
    [Column("short_name")]
    public string ShortName { get; set; }
}
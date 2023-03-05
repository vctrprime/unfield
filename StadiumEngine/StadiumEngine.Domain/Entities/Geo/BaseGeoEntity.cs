using System.ComponentModel.DataAnnotations.Schema;

namespace StadiumEngine.Domain.Entities.Geo;

public class BaseGeoEntity : BaseEntity
{
    [Column( "short_name" )] public string ShortName { get; set; }
}
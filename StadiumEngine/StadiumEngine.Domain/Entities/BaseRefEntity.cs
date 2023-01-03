using System.ComponentModel.DataAnnotations.Schema;

namespace StadiumEngine.Domain.Entities;

public class BaseRefEntity : BaseUserEntity
{
    [NotMapped]
    public new string Name { get; set; }
    [NotMapped]
    public new string Description { get; set; }
}
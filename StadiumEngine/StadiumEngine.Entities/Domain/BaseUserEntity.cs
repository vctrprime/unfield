using System.ComponentModel.DataAnnotations.Schema;

namespace StadiumEngine.Entities.Domain;

public class BaseUserEntity : BaseEntity
{
    [Column("user_created_id")]
    public int? UserCreatedId { get; set; }

    [Column("user_modified_id")]
    public int? UserModifiedId { get; set; }
    
}
using System.ComponentModel.DataAnnotations.Schema;
using StadiumEngine.Entities.Domain.Accounts;

namespace StadiumEngine.Entities.Domain;

public class BaseUserEntity : BaseEntity
{
    [Column("user_created_id")]
    [ForeignKey("UserCreated")]
    public int? UserCreatedId { get; set; }

    [Column("user_modified_id")]
    [ForeignKey("UserModified")]
    public int? UserModifiedId { get; set; }
    
    public virtual User UserCreated { get; set; }
    public virtual User UserModified { get; set; }
}
using System.ComponentModel.DataAnnotations.Schema;
using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Domain.Entities.Rates;

public abstract class BaseRateEntity : BaseUserEntity
{
    [Column( "stadium_id" )] public int StadiumId { get; set; }

    [ForeignKey( "StadiumId" )] public virtual Stadium Stadium { get; set; }

    [Column( "is_active" )] public bool IsActive { get; set; }

    [Column( "is_deleted" )] public bool IsDeleted { get; set; }
}
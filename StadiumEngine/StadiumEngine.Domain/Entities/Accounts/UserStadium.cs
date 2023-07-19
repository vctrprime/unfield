using System.ComponentModel.DataAnnotations.Schema;

namespace StadiumEngine.Domain.Entities.Accounts;

[Table( "user_stadium", Schema = "accounts" )]
public class UserStadium : BaseRefEntity
{
    [Column( "user_id" )]
    public int UserId { get; set; }

    [ForeignKey( "UserId" )]
    public virtual User User { get; set; }

    [Column( "stadium_id" )]
    public int StadiumId { get; set; }

    [ForeignKey( "StadiumId" )]
    public virtual Stadium Stadium { get; set; }
}
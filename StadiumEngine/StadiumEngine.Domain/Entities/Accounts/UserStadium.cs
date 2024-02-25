namespace StadiumEngine.Domain.Entities.Accounts;

public class UserStadium : BaseUserEntity
{
    public int UserId { get; set; }
    public virtual User User { get; set; }
    public int StadiumId { get; set; }
    public virtual Stadium Stadium { get; set; }
}
#nullable enable
using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Domain.Entities.Customers;

public class Customer : BaseEntity
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string PhoneNumber { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Language { get; set; } = null!;
    public int StadiumId { get; set; }
    
    public virtual Stadium Stadium { get; set; }
}
#nullable enable
namespace StadiumEngine.Domain.Entities.Customers;

public class Customer : BaseEntity
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string PhoneNumber { get; set; } = null!;
    
    public string Language { get; set; } = null!;
}
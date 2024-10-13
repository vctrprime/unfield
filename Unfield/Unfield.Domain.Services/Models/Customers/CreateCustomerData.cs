using Unfield.Domain.Entities.Accounts;

namespace Unfield.Domain.Services.Models.Customers;

public class CreateCustomerData
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string PhoneNumber { get; set; } = null!;
    public string Language { get; set; } = null!;
    public Stadium Stadium { get; set; } = null!;
}
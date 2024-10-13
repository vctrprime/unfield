#nullable enable
using System;
using System.Data.Common;
using Unfield.Domain.Entities.Accounts;

namespace Unfield.Domain.Entities.Customers;

public class Customer : BaseEntity
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string PhoneNumber { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Language { get; set; } = null!;
    public int StadiumGroupId { get; set; }
    public DateTime? LastLoginDate { get; set; }
    
    public virtual StadiumGroup StadiumGroup { get; set; }
}
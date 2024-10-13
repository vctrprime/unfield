using System;
using System.Collections.Generic;
using Unfield.Domain.Entities.Accounts;

namespace Unfield.Domain.Entities.Settings;

public class Break : BaseUserEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int StadiumId { get; set; }
    public decimal StartHour { get; set; }
    public decimal EndHour { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime DateStart { get; set; }
    public DateTime? DateEnd { get; set; }
    
    public virtual Stadium Stadium { get; set; }
    
    public virtual ICollection<BreakField> BreakFields { get; set; }

}
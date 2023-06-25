using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Domain.Entities.Settings;

[Table( "break", Schema = "settings" )]
public class Break : BaseUserEntity
{
    [Column( "stadium_id" )]
    public int StadiumId { get; set; }
    
    [Column( "start_hour" )]
    public decimal StartHour { get; set; }
    
    [Column( "end_hour" )]
    public decimal EndHour { get; set; }
    
    [Column( "is_active" )]
    public bool IsActive { get; set; }

    [Column( "is_deleted" )]
    public bool IsDeleted { get; set; }
    
    [Column( "date_start", TypeName = "timestamp without time zone" )]
    public DateTime DateStart { get; set; }

    [Column( "date_end", TypeName = "timestamp without time zone" )]
    public DateTime? DateEnd { get; set; }
    
    [ForeignKey( "StadiumId" )]
    public virtual Stadium Stadium { get; set; }
    
    public virtual ICollection<BreakField> BreakFields { get; set; }

}
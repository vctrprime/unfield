using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StadiumEngine.Domain.Entities;

public abstract class BaseEntity
{
    [DatabaseGenerated( DatabaseGeneratedOption.Identity )]
    [Key]
    [Column( "id", Order = 0 )]
    public int Id { get; set; }

    [Column( "name" )]
    public string Name { get; set; }

    [Column( "description" )]
    public string Description { get; set; }

    [Column( "date_created" )]
    public DateTime DateCreated { get; set; }

    [Column( "date_modified" )]
    public DateTime? DateModified { get; set; }
}
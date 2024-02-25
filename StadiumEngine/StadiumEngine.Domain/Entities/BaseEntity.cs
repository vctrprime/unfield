using System;

namespace StadiumEngine.Domain.Entities;

public abstract class BaseEntity
{
    public int Id { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime? DateModified { get; set; }
}
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BpChallenge.Domain.Entities;

public class BaseEntity
{
    [Column("id")]
    public int Id { get; set; }
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
}

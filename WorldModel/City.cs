using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WorldModel;

[Table("City")]
public partial class City
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    public int CountryIdentifier { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    public int Latitude { get; set; }

    public int Longitude { get; set; }

    [Column("population")]
    public int Population { get; set; }

    [ForeignKey("CountryIdentifier")]
    [InverseProperty("Cities")]
    public virtual Country CountryIdentifierNavigation { get; set; } = null!;
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WorldModel;

[Table("Country")]
public partial class Country
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("NAME")]
    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [Column("ISO2")]
    [StringLength(2)]
    [Unicode(false)]
    public string Iso2 { get; set; } = null!;

    [Column("ISO3")]
    [StringLength(3)]
    [Unicode(false)]
    public string Iso3 { get; set; } = null!;

    [InverseProperty("CountryIdentifierNavigation")]
    public virtual ICollection<City> Cities { get; set; } = new List<City>();
}

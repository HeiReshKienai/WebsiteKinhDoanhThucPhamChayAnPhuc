using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebsiteKinhDoanhThucPhamChayAnPhuc.Models;

public partial class Catology
{
    public int IdCat { get; set; }
    [Required, StringLength(100)]
    public string? NameCat { get; set; }
    [Range(0.01, 10000.00)]
    public int? Order { get; set; }

    public string? Link { get; set; }

    public int? Hide { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}

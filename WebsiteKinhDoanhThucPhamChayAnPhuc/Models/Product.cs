using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebsiteKinhDoanhThucPhamChayAnPhuc.Models;

public partial class Product
{
    public int IdPro { get; set; }
    [Required, StringLength(100)]
    public string? NamePro { get; set; }
    [Range(0.01, 10000.00)]
    public int? Nums { get; set; }

    public decimal? Price { get; set; }

    public string? Detail { get; set; }

    public string? Img1 { get; set; }

    public string? Img2 { get; set; }

    public string? Img3 { get; set; }

    public int? Order { get; set; }

    public string? Link { get; set; }

    public int? Hide { get; set; }

    public int? IdCat { get; set; }

    public virtual ICollection<CartDetail> CartDetails { get; set; } = new List<CartDetail>();

    public virtual Catology? IdCatNavigation { get; set; }
}

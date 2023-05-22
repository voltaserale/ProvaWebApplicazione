using System;
using System.Collections.Generic;

namespace ProvaWebApplicazione.Models;

public partial class Category
{
    public short CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public string? Description { get; set; }

    public byte[]? Pictures { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}

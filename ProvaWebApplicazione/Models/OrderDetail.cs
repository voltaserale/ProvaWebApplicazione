using System;
using System.Collections.Generic;

namespace ProvaWebApplicazione.Models;

public partial class OrderDetail
{
    public short OrderId { get; set; }

    public short ProductId { get; set; }

    public double UnitPrice { get; set; }

    public short Quantity { get; set; }

    public double Discount { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}

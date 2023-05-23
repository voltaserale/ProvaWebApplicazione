using System;
using System.Collections.Generic;

namespace ProvaWebApplicazione.Models;

public partial class Vistaordini
{
    public short OrderId { get; set; }

    public DateTime? OrderDate { get; set; }

    public string Customer { get; set; } = null!;

    public string ProductName { get; set; } = null!;

    public short Quantity { get; set; }

    public string? Employee { get; set; }
}

﻿using System;
using System.Collections.Generic;

namespace ProvaWebApplicazione.Models;

public partial class Shipper
{
    public short ShipperId { get; set; }

    public string CompanyName { get; set; } = null!;

    public string? Phone { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}

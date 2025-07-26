using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class Promotion
{
    public int Id { get; set; }

    public string DiscountCode { get; set; } = null!;

    public int DiscountPercent { get; set; }

    public DateOnly ExpiryDate { get; set; }

    public int? ClusterId { get; set; }

    public virtual PitchCluster? Cluster { get; set; }
}

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Backend.Models;

public partial class Promotion
{
    public int Id { get; set; }

    public string DiscountCode { get; set; } = null!;

    public int DiscountPercent { get; set; }

    public DateOnly ExpiryDate { get; set; }

    public int? ClusterId { get; set; }

    [JsonIgnore]
    public virtual PitchCluster? Cluster { get; set; }
}

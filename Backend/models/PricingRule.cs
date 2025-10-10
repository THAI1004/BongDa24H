using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Backend.Models;

public partial class PricingRule
{
    public int Id { get; set; }

    public int PitchId { get; set; }

    public string TimeSlot { get; set; } = null!;

    public int Price { get; set; }

    [JsonIgnore]
    public virtual Pitch Pitch { get; set; } = null!;
}

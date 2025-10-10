using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Backend.Models;

public partial class Pitch
{
    public int Id { get; set; }

    public string PitchName { get; set; } = null!;

    public int ClusterId { get; set; }

    public string? ImageUrl { get; set; }

    public string? Facilities { get; set; }

    public int? PitchType { get; set; }

    [JsonIgnore]
    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual PitchCluster Cluster { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<MatchRequest> MatchRequests { get; set; } = new List<MatchRequest>();

    [JsonIgnore]
    public virtual ICollection<PricingRule> PricingRules { get; set; } = new List<PricingRule>();
}

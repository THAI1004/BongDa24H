using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class Pitch
{
    public int Id { get; set; }

    public string PitchName { get; set; } = null!;

    public int ClusterId { get; set; }

    public string PitchType { get; set; } = null!;

    public string? ImageUrl { get; set; }

    public string? Facilities { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual PitchCluster Cluster { get; set; } = null!;

    public virtual ICollection<MatchRequest> MatchRequests { get; set; } = new List<MatchRequest>();

    public virtual ICollection<PricingRule> PricingRules { get; set; } = new List<PricingRule>();
}

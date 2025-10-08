using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class PitchCluster
{
    public int Id { get; set; }

    public string ClusterName { get; set; } = null!;

    public string Address { get; set; } = null!;

    public double? Longitude { get; set; }

    public double? Latitude { get; set; }

    public int OwnerId { get; set; }
    public string? Image { get; set; }

    public virtual User Owner { get; set; } = null!;

    public virtual ICollection<Pitch> Pitches { get; set; } = new List<Pitch>();

    public virtual ICollection<Promotion> Promotions { get; set; } = new List<Promotion>();
}

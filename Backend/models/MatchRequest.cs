using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Backend.Models;

public partial class MatchRequest
{
    public int Id { get; set; }

    public int CreatorId { get; set; }

    public int? PitchId { get; set; }

    public DateOnly MatchDate { get; set; }

    public string TimeSlot { get; set; } = null!;

    public int? SkillLevel { get; set; }

    public virtual User Creator { get; set; } = null!;

    public virtual ICollection<MatchResponse> MatchResponses { get; set; } = new List<MatchResponse>();

    public virtual Pitch? Pitch { get; set; }
}

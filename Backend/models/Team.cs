using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class Team
{
    public int Id { get; set; }

    public string TeamName { get; set; } = null!;

    public int ManagerId { get; set; }

    public int? TotalMatches { get; set; }

    public int? Wins { get; set; }

    public int? SkillLevel { get; set; }
    public string? Image { get; set; }

    public virtual User Manager { get; set; } = null!;
}

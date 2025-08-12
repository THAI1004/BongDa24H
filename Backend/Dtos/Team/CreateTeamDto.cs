namespace Backend.Dtos;

public class CreateTeamDto
{
    public string TeamName { get; set; } = null!;

    public int ManagerId { get; set; }

    public int? TotalMatches { get; set; }

    public int? Wins { get; set; }

    public int? SkillLevel { get; set; }

}
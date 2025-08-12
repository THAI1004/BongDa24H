namespace Backend.Dtos.MatchRequest;

public class CreateMatchRequestDto
{
    public int CreatorId { get; set; }

    public int? PitchId { get; set; }

    public DateOnly MatchDate { get; set; }

    public string TimeSlot { get; set; } = null!;

    public int? SkillLevel { get; set; }
}
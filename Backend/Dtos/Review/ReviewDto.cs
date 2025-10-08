namespace Backend.Dtos;

public class ReviewDto
{
    public int BookingId { get; set; }

    public int? PitchRating { get; set; }

    public int? OpponentRating { get; set; }

    public string? OpponentSkill { get; set; }

    public string? OpponentAttitude { get; set; }

    public string? Comment { get; set; }
}
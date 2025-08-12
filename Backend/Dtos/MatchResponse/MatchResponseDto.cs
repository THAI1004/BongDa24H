namespace Backend.Dtos.MatchResponse;

public class MatchResponseDto
{

    public int RequestId { get; set; }

    public int ResponderId { get; set; }

    public string? Content { get; set; }

    public int? Status { get; set; }

}
using Backend.Models;

namespace Backend.Dtos.Pitch;

public class PitchDto
{
    public int Id { get; set; }


    public string PitchName { get; set; } = null!;

    public int ClusterId { get; set; }

    public string? ImageUrl { get; set; }

    public string? Facilities { get; set; }

    public int? PitchType { get; set; }

}
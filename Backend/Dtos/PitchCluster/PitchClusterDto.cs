using Backend.Models;

namespace Backend.Dtos.PitchCluster;

public class PitchClusterDto
{
    public int Id { get; set; }

    public string ClusterName { get; set; } = null!;

    public string Address { get; set; } = null!;

    public double? Longitude { get; set; }

    public double? Latitude { get; set; }

    public int OwnerId { get; set; }
    public string? Image { get; set; }
}
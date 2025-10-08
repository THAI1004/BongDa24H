using Backend.Dtos.PitchCluster;
using Backend.Models;
namespace Backend.Mappers;

public static class PitchClusterMapper
{
    public static PitchClusterDto ToPitchClusterDto(this PitchCluster pitchCluster)
    {
        return new PitchClusterDto
        {
            Id = pitchCluster.Id,
            ClusterName = pitchCluster.ClusterName,
            Address = pitchCluster.Address,
            Longitude = pitchCluster.Longitude,
            Latitude = pitchCluster.Latitude,
            OwnerId = pitchCluster.OwnerId
            ,
            Image = pitchCluster.Image
        };
    }
    public static PitchCluster ToPitchCluster(this CreatePitchClusterDto pitchCluster)
    {
        return new PitchCluster
        {
            ClusterName = pitchCluster.ClusterName,
            Address = pitchCluster.Address,
            Longitude = pitchCluster.Longitude,
            Latitude = pitchCluster.Latitude,
            OwnerId = pitchCluster.OwnerId
            ,
            Image = pitchCluster.Image
        };
    }
}
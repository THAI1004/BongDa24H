using Backend.Dtos.PitchCluster;

namespace Backend.Interfaces;

public interface IPitchClusterRepository
{
    Task<List<PitchClusterDto>> GetAllPitchCluster();
    Task<PitchClusterDto> GetPitchClusterById(int id);
}
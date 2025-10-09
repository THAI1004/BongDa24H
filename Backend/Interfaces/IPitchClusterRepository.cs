using Backend.Dtos.PitchCluster;
using Backend.Models;

namespace Backend.Interfaces;

public interface IPitchClusterRepository
{
    Task<List<PitchCluster>> GetAllPitchCluster();
    Task<PitchCluster> GetPitchClusterById(int id);
    Task<PitchCluster> Create(PitchCluster pitchCluster);
    Task<PitchCluster> Update(PitchCluster pitchCluster);
    Task<bool> Delete(PitchCluster pitchCluster);

}
using Backend.Dtos.Pitch;
using Backend.Models;

namespace Backend.Interfaces;

public interface IPitchRepository
{
    Task<List<PitchDto>> GetAllPitchesAsync();
    Task<Pitch> GetPitchByIdAsync(int id);
    Task<CreatePitchDto> CreatePitchAsync(CreatePitchDto createPitchDto);
    Task<Pitch> UpdatePitchAsync(Pitch pitch);
    Task<bool> DeletePitchAsync(int id);
}
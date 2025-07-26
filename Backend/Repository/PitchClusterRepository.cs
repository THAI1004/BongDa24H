using Backend.Dtos.PitchCluster;
using Backend.Interfaces;
using Backend.Mappers;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository;

public class PitchClusterRepository : IPitchClusterRepository
{
    private readonly BongDa24HContext _context;

    public PitchClusterRepository(BongDa24HContext context)
    {
        _context = context;
    }

    public async Task<List<PitchClusterDto>> GetAllPitchCluster()
    {
        return await _context.PitchClusters
            .Select(pc => pc.ToPitchClusterDto())
            .ToListAsync();
    }
    public async Task<PitchClusterDto> GetPitchClusterById(int id)
    {
        return await _context.PitchClusters
           .Where(pc => pc.Id == id)
           .Select(pc => pc.ToPitchClusterDto())
           .FirstOrDefaultAsync();
    }
}
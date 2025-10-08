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
        return await _context.PitchClusters.Include(pc => pc.Pitches).Include(pc => pc.Owner)
            .Select(pc => pc.ToPitchClusterDto())
            .ToListAsync();
    }
    public async Task<PitchCluster> GetPitchClusterById(int id)
    {
        var pc = await _context.PitchClusters.Include(pc => pc.Pitches).Include(pc => pc.Owner)
           .FirstOrDefaultAsync(pc => pc.Id == id);
        return pc;
    }
    public async Task<PitchCluster> Create(PitchCluster pitchCluster)
    {
        _context.PitchClusters.Add(pitchCluster);
        await _context.SaveChangesAsync();
        return pitchCluster;
    }
    public async Task<PitchCluster> Update(PitchCluster pitchCluster)
    {
        _context.PitchClusters.Update(pitchCluster);
        await _context.SaveChangesAsync();
        return pitchCluster;
    }
    public async Task<bool> Delete(PitchCluster pitchCluster)

    {
        _context.Pitches.RemoveRange(pitchCluster.Pitches);
        _context.PitchClusters.Remove(pitchCluster);
        await _context.SaveChangesAsync();
        return true;
    }
}
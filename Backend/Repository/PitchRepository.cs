using Backend.Dtos.Pitch;
using Backend.Interfaces;
using Backend.Mappers;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository;

public class PitchRepository : IPitchRepository
{
    private readonly BongDa24HContext _context;
    public PitchRepository(BongDa24HContext context)
    {
        _context = context;
    }
    public async Task<CreatePitchDto> CreatePitchAsync(CreatePitchDto createPitchDto)
    {
        await _context.Pitches.AddAsync(createPitchDto.ToPitchCreate());
        await _context.SaveChangesAsync();
        return createPitchDto;
    }

    public async Task<bool> DeletePitchAsync(int id)
    {
        var pitch = _context.Pitches.FirstOrDefaultAsync(p => p.Id == id);
        if (pitch == null)
        {
            throw new KeyNotFoundException($"Pitch không tồn tại với ID: {id}");
        }
        _context.Pitches.Remove(pitch.Result);
        await _context.SaveChangesAsync();
        return true;

    }

    public async Task<List<PitchDto>> GetAllPitchesAsync()
    {
        return await _context.Pitches.
            Select(p => p.ToPitchDto())
            .ToListAsync();
    }

    public async Task<Pitch> GetPitchByIdAsync(int id)
    {
        var pitch = await _context.Pitches
        .FirstOrDefaultAsync(p => p.Id == id);
        if (pitch == null)
        {
            throw new KeyNotFoundException($"Pitch không tồn tại với ID: {id}");
        }
        return pitch;
    }

    public async Task<Pitch> UpdatePitchAsync(Pitch pitch)
    {
        _context.Pitches.Update(pitch);
        await _context.SaveChangesAsync();
        return pitch;
    }
}
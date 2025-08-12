using Backend.Dtos.MatchResponse;
using Backend.Interfaces;
using Backend.Mappers;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository;

public class MatchResponseRepository : IMatchResponseRepository
{
    private readonly BongDa24HContext _context;
    public MatchResponseRepository(BongDa24HContext context)
    {
        _context = context;
    }
    public async Task<MatchResponseDto> CreateMatchResponseAsyn(MatchResponseDto matchResponseDto)
    {
        _context.MatchResponses.Add(matchResponseDto.ToMatchResponse());
        await _context.SaveChangesAsync();
        return matchResponseDto;
    }

    public async Task<MatchResponse?> GetMatchResponseById(int responseId)
    {
        return await _context.MatchResponses.FirstOrDefaultAsync(m => m.Id == responseId);
    }

    public async Task<MatchResponse> UpdateMatchResponseAsyn(MatchResponse matchResponse)
    {
        _context.MatchResponses.Update(matchResponse);
        await _context.SaveChangesAsync();
        return matchResponse;
    }
}
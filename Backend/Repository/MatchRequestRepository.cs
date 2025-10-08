using Backend.Dtos.MatchRequest;
using Backend.Interfaces;
using Backend.Mappers;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository;

public class MatchRequestRepository : IMatchRequestRepository
{
    private readonly BongDa24HContext _context;


    public MatchRequestRepository(BongDa24HContext context)
    {
        _context = context;
    }
    public async Task<CreateMatchRequestDto> CreateMatchRequestAsync(CreateMatchRequestDto matchRequest)
    {
        _context.MatchRequests.Add(matchRequest.ToCreateMatchRequest());
        await _context.SaveChangesAsync();
        return matchRequest;
    }
    public async Task<List<MatchRequest>> GetMatchRequestsAsync(int pitchId)
    {
        return await _context.MatchRequests.Where(m => m.PitchId == pitchId).ToListAsync();
    }
    public async Task<List<MatchRequest>> GetAllMatchRequest()
    {
        return await _context.MatchRequests.Include(m => m.Creator).ThenInclude(u => u.Teams)
    .Include(m => m.Pitch)
    .ThenInclude(p => p.Cluster)
    .Include(m => m.MatchResponses)
        .ThenInclude(r => r.Responder)   // load cả user phản hồi
    .ToListAsync(); ;
    }


}
using Backend.Dtos.MatchRequest;
using Backend.Models;

namespace Backend.Interfaces;

public interface IMatchRequestRepository
{
    // Define methods for match request operations, e.g., Create, Read, Update, Delete
    Task<CreateMatchRequestDto> CreateMatchRequestAsync(CreateMatchRequestDto matchRequest);
    Task<List<MatchRequest>> GetMatchRequestsAsync(int pitchId);
    Task<List<MatchRequest>> GetAllMatchRequest();

}
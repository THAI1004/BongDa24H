using Backend.Dtos.MatchResponse;
using Backend.Models;

namespace Backend.Interfaces;

public interface IMatchResponseRepository
{
    Task<MatchResponseDto> CreateMatchResponseAsyn(MatchResponseDto matchResponseDto);
    Task<MatchResponse> UpdateMatchResponseAsyn(MatchResponse matchResponseDto);
    Task<MatchResponse?> GetMatchResponseById(int responseId);
}
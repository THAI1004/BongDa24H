using Backend.Dtos.MatchResponse;
using Backend.Models;

namespace Backend.Mappers;

public static class MatchResponseMapper
{
    public static MatchResponse ToMatchResponse(this MatchResponseDto matchResponseDto)
    {
        return new MatchResponse
        {
            RequestId = matchResponseDto.RequestId,
            ResponderId = matchResponseDto.ResponderId,
            Content = matchResponseDto.Content,
            Status = matchResponseDto.Status
        };
    }
}
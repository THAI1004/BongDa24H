using Backend.Dtos.MatchRequest;
using Backend.Models;

namespace Backend.Mappers;

public static class MatchRequestMapper
{
    public static MatchRequest ToCreateMatchRequest(this CreateMatchRequestDto dto)
    {
        return new MatchRequest
        {
            CreatorId = dto.CreatorId,
            PitchId = dto.PitchId,
            MatchDate = dto.MatchDate,
            TimeSlot = dto.TimeSlot,
            SkillLevel = dto.SkillLevel
        };
    }
}
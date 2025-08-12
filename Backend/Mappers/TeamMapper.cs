using Backend.Dtos;
using Backend.Models;

namespace Backend.Mappers;

public static class TeamMapper
{
    public static Team ToCreateTeamDto(this CreateTeamDto createTeamDto)
    {
        return new Team
        {
            TeamName = createTeamDto.TeamName,
            ManagerId = createTeamDto.ManagerId,
            TotalMatches = createTeamDto.TotalMatches,
            Wins = createTeamDto.Wins,
            SkillLevel = createTeamDto.SkillLevel
        };
    }
}
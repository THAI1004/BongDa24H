using Backend.Dtos;
using Backend.Models;

namespace Backend.Interfaces;

public interface ITeamRepository
{
    Task<List<Team>> GetAllTeamAsyn();
    Task<CreateTeamDto> CreateTeamAsyn(CreateTeamDto createTeamDto);
    Task<Team?> GetTeamByIdAsyn(int Id);
    Task<Team> UpdateTeamAsyn(Team team);
    Task<Team?> DeleteTeam(int id);
}